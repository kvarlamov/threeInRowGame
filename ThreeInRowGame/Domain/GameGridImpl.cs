using ThreeInRowGame.Domain.Base;
using ThreeInRowGame.Domain.ValueObjects;

namespace ThreeInRowGame.Domain;

public class GameGridImpl : GameGridBase
{
    public int LastMatched;
    
    private readonly Element[,] _grid;
    private readonly Dictionary<int, int> _xIndexMap;
    private readonly Dictionary<char, int> _yIndexMap;

    public GameGridImpl(int xSize = 9, int ySize = 9)
    {
        _grid = new Element[xSize, ySize];
        
        _xIndexMap = new Dictionary<int, int>();
        for (int i = 1; i < xSize; i++)
        {
            _xIndexMap[i] = i;
        }

        _yIndexMap = new Dictionary<char, int>();
        char yStart = 'A';
        for (int i = 1; i < ySize; i++)
        {
            _yIndexMap[(char)(yStart + i - 1)] = i;
        }

        for (int i = 1; i < _grid.GetLength(1); i++)
        {
            _grid[i, 0] = Element.Create(i.ToString());
        }
        
        for (int i = 1; i < _grid.GetLength(0); i++)
        {
            _grid[0, i] = Element.Create(((char)(yStart + i - 1)).ToString());
        }

        _grid[0, 0] = Element.Clear();

    }

    public Element this[int x, char y]
    {
        get
        {
            if (!_xIndexMap.ContainsKey(x))
                throw new IndexOutOfRangeException($"Invalid x index: {x}");
            if (!_yIndexMap.ContainsKey(y))
                throw new IndexOutOfRangeException($"Invalid y index: {y}");

            var xIndex = _xIndexMap[x];
            var yIndex = _yIndexMap[y];
            return _grid[_xIndexMap[x], _yIndexMap[y]];
        }
        set
        {
            if (!_xIndexMap.ContainsKey(x))
                throw new IndexOutOfRangeException($"Invalid x index: {x}");
            if (!_yIndexMap.ContainsKey(y))
                throw new IndexOutOfRangeException($"Invalid y index: {y}");
                
            _grid[_xIndexMap[x], _yIndexMap[y]] = value;
        }
    }

    public override void Initialize()
    {
        for (int i = 1; i < _grid.GetLength(0); i++)
        {
            for (int j = 1; j < _grid.GetLength(1); j++)
            {
                _grid[i, j] = ElementFactory.GetNext();
            }
        }
    }

    public override void ChangeElements(string input)
    {
        // Parse the input string
        char row1 = input[0];
        int col1 = int.Parse(input[1].ToString());

        char row2 = input[2];
        int col2 = int.Parse(input[3].ToString());

        (this[col2, row2], this[col1, row1]) = (this[col1, row1], this[col2, row2]);
    }

    protected override void UpdateOnTurn()
    {
        int rows = _grid.GetLength(0);
        int cols = _grid.GetLength(1);

        for (int col = 1; col < cols; col++)
        {
            int emptyRow = rows - 1; // Start from the bottom row

            // Traverse the column from bottom to top
            for (int row = rows - 1; row >= 1; row--)
            {
                if (string.IsNullOrWhiteSpace(_grid[row, col].Value))
                {
                    // Move the element down to the lowest empty position
                    _grid[emptyRow, col] = _grid[row, col];

                    // If the element was moved, mark the original position as empty
                    if (emptyRow != row)
                    {
                        _grid[row, col] = Element.Clear();
                    }

                    emptyRow--; // Move the pointer for the next empty row up
                }
            }
        }
    }

    public override void ReDrawFields()
    {
        do
        {
            UpdateOnTurn();
            //Draw();//TODO - временно для отладки, УДАЛИТЬ
        } while (CheckThreeInRowAndClear(true));
        
        for (int i = 1; i < _grid.GetLength(0); i++)
        {
            for (int j = 1; j < _grid.GetLength(1); j++)
            {
                if (!string.IsNullOrWhiteSpace(_grid[i,j].Value))
                    continue;
                
                _grid[i, j] = ElementFactory.GetNext();
            }
        }
        Draw();
    }

    public override void Draw()
    {
        Console.WriteLine("\n\n");
        for (int i = 0; i < _grid.GetLength(0); i++)
        {
            for (int j = 0; j < _grid.GetLength(1); j++)
            {
                Console.ForegroundColor = _grid[i, j].Color;
                Console.Write($"{_grid[i, j]} ");
            }
            Console.WriteLine();
        }
    }

    public override bool CheckThreeInRowAndClear(bool redraw = false)
    {
        if (!redraw)
            LastMatched = 0;
        
        var matchedPositions = new List<(int, int)>();

        // Check horizontal matches
        for (int row = 1; row < _grid.GetLength(0); row++)
        {
            int matchLength = 1;
            for (int col = 1; col < _grid.GetLength(1); col++)
            {
                if (_grid[row, col].Equals(_grid[row, col - 1]))
                {
                    matchLength++;
                }
                else
                {
                    if (matchLength >= 3)
                    {
                        for (int k = 1; k < matchLength; k++)
                        {
                            matchedPositions.Add((row, col - 1 - k));
                        }
                    }
                    matchLength = 1;
                }
            }

            if (matchLength >= 3)
            {
                for (int k = 1; k < matchLength; k++)
                {
                    matchedPositions.Add((row, _grid.GetLength(1) - 1 - k));
                }
            }
        }

        // Check vertical matches
        for (int col = 1; col < _grid.GetLength(1); col++)
        {
            int matchLength = 1;
            for (int row = 2; row < _grid.GetLength(0); row++)
            {
                if (_grid[row, col].Equals(_grid[row - 1, col]))
                {
                    matchLength++;
                }
                else
                {
                    if (matchLength >= 3)
                    {
                        for (int k = 1; k < matchLength; k++)
                        {
                            matchedPositions.Add((row - 1 - k, col));
                        }
                    }
                    matchLength = 1;
                }
            }

            if (matchLength >= 3)
            {
                for (int k = 1; k < matchLength; k++)
                {
                    matchedPositions.Add((_grid.GetLength(0) - 1 - k, col));
                }
            }
        }

        foreach ((int, int) position in matchedPositions)
        {
            _grid[position.Item1, position.Item2] = Element.Clear();
        };

        LastMatched += matchedPositions.Count;

        return matchedPositions.Count > 0;
    }

    public override bool IsNeighboors(string input)
    {
        // Parse the input string
        char row1 = input[0];
        int col1 = int.Parse(input[1].ToString());

        char row2 = input[2];
        int col2 = int.Parse(input[3].ToString());

        // Convert row letters to numerical indices (A -> 0, B -> 1, C -> 2, etc.)
        int rowIndex1 = row1 - 'A';
        int rowIndex2 = row2 - 'A';

        // Check if the cells are neighbors (adjacent)
        bool isHorizontalNeighbor = (rowIndex1 == rowIndex2) && (Math.Abs(col1 - col2) == 1);
        bool isVerticalNeighbor = (col1 == col2) && (Math.Abs(rowIndex1 - rowIndex2) == 1);

        return isHorizontalNeighbor || isVerticalNeighbor;
    }
}