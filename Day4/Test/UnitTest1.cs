using NUnit.Framework.Constraints;

namespace Day4;

public class Tests
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
        Assert.That(grid.Get(new Position(1, 1)) , Is.EqualTo("."));
    }
    
    
    public void CellIsEmpty()
    {
        Grid grid = new Grid(".@." +
                             "@@.");
        var paperRoll = grid.Get(new Position(1, 1));
        Assert.That(paperRoll.State, Is.EqualTo(PaperRollState.Empty));
    }
    
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
        
    }
    
    
}

public class Position(int row, int column)
{
    public int Row { get; } = row;
    public int Column { get; } = column;
}

public class Grid
{
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

