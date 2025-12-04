using NUnit.Framework.Constraints;

namespace Day4;

public class Test
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GridIsCreatedCorrected()
    {
        Grid grid = new Grid(".@." +
                             "@@.");
        Assert.That(grid.Rows , Is.EqualTo(2));
        Assert.That(grid.Get(new Position(1, 1)).State , Is.EqualTo(PaperRollState.Empty));
    }
    
    [Test]
    public void CellIsEmpty()
    {
        Grid grid = new Grid(".@." +
                             "@@.");
        var paperRoll = grid.Get(new Position(1, 1));
        Assert.That(paperRoll.State, Is.EqualTo(PaperRollState.Empty));
    }
    
    [Test]
    public void CellIsFull()
    {
        Grid grid = new Grid(".@." +
                             "@@.");
        var paperRoll = grid.Get(new Position(1, 2));
        Assert.That(paperRoll.State, Is.EqualTo(PaperRollState.Full));
    }
    
    [Test]
    public void GetNeighboursOfCell()
    {
        Grid grid = new Grid(".@." +
                             "@@.");
        var neighbours = grid.GetNeighbours(new Position(1, 2)).ToList();
        Assert.That(neighbours, Is.Not.Empty);
        Assert.That(neighbours, Has.Count.EqualTo(2));
        
    }
    
    
}

public class Position(int row, int column)
{
    public int Row { get; } = row;
    public int Column { get; } = column;
}

public class Grid
{
    private const int FullPaperRoll = '@';
    public List<string> Cells { get; set; }

    public Grid(string gridLayout)
    {
        Cells = gridLayout.Split("\n").ToList();
        
    }

    public int Rows()
    {
        return Cells.Count;
    }

    public PaperRoll Get(Position position)
    {
        var row = position.Row;
        var column = position.Column;
        return new PaperRoll(Cells[row][column].ToString());
    }

    public IEnumerable<PaperRoll> GetNeighbours(Position position)
    {
        for (var x = 0; x < Cells.Count; x++)
        {
            for (var y = 0; y < Cells.Count; y++)
            {
                if (x <= position.Row + 1 && x >= position.Row - 1)
                {
                    if (y <= position.Row + 1 && y >= position.Row - 1)
                    {
                        if (Cells[x][y] == FullPaperRoll)
                        {
                            yield return new PaperRoll(Cells[x][y].ToString());
                        }

                    }       
                }
            }
        }
    }
}

public class PaperRoll
{
    public PaperRollState State = PaperRollState.Empty;
    public PaperRoll(string value)
    {
        
        switch (value)
        {
            case ".":
                State = PaperRollState.Empty;
                break;
            case "@":
                State = PaperRollState.Full;
                break;
            default:
                State = PaperRollState.Empty;
                break;
        }
    }
}

public enum PaperRollState
{
    Empty,
    Full
}

