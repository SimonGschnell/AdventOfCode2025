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
        Grid grid = new Grid([".@.","@@."]);
        Assert.That(grid.Rows , Is.EqualTo(2));
        Assert.That(grid.Get(new Position(0,0)).State , Is.EqualTo(PaperRollState.Empty));
    }
    
    [Test]
    public void CellIsEmpty()
    {
        Grid grid = new Grid([".@.","@@."]);
        var paperRoll = grid.Get(new Position(0,0));
        Assert.That(paperRoll.State, Is.EqualTo(PaperRollState.Empty));
    }
    
    [Test]
    public void CellIsFull()
    {
        Grid grid = new Grid([".@.","@@."]);
        var paperRoll = grid.Get(new Position(0,1));
        Assert.That(paperRoll.State, Is.EqualTo(PaperRollState.Full));
    }
    
    [Test]
    public void GetNeighboursOfCell()
    {
        Grid grid = new Grid([
            ".@.",
            "@@."]);
        var neighbours = grid.GetNeighbours(new Position(1,2)).ToList();
        Assert.That(neighbours, Is.Not.Empty);
        Assert.That(neighbours, Has.Count.EqualTo(2));
    }
    
    [Test]
    public void CanBeAccessedByAForklift()
    {
        Grid grid = new Grid([
            ".@@@",
            "@@@."]);
        Assert.That(grid.CanBeAccessedByAForklift(new Position(0, 1)),Is.False);
        Assert.That(grid.CanBeAccessedByAForklift(new Position(0, 3)),Is.True);
    }
    
    [Test]
    public void CanBeAccessedByAForkliftCount()
    {
        Grid grid = new Grid([
            ".@@@",
            "@@@."]);
        Assert.That(grid.CanBeAccessedByAForkliftCount(),Is.EqualTo(2));
    }
    
    [Test]
    public void PassesAllDay4TestResults()
    {
        Grid grid = new Grid([
            "..@@.@@@@.",
            "@@@.@.@.@@",
            "@@@@@.@.@@",
            "@.@@@@..@.",
            "@@.@@@@.@@",
            ".@@@@@@@.@",
            ".@.@.@.@@@",
            "@.@@@.@@@@",
            ".@@@@@@@@.",
            "@.@.@@@.@."]);
        Assert.That(grid.CanBeAccessedByAForkliftCount(),Is.EqualTo(13));
    }
    
    [Test]
    public void ClaculateTheSolutionForDay4Part1()
    {
        const string day4InputTxt = "Day4Input.txt";
        Grid grid = new Grid(File.ReadAllLines(day4InputTxt).ToList());
        
        Assert.That(grid.CanBeAccessedByAForkliftCount(),Is.EqualTo(1395));
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
    
    public Grid(List<string> rows)
    {
        Cells = rows;
        
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
            for (var y = 0; y < Cells[x].Length; y++)
            {
                if (x == position.Row && y == position.Column)
                {
                    continue;
                }
                if (x <= position.Row + 1 && x >= position.Row - 1)
                {
                    if (y <= position.Column + 1 && y >= position.Column - 1)
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

    public bool CanBeAccessedByAForklift(Position position)
    {
        if (!IsPositionFullPaperRoll(position))
        {
            return false;
        }
        return GetNeighbours(position).ToList().Count < 4;
    }

    private bool IsPositionFullPaperRoll(Position position)
    {
        return Cells[position.Row][position.Column] == FullPaperRoll;
    }
    
    public int CanBeAccessedByAForkliftCount()
    {
        var result = 0;
        for (var x = 0; x < Cells.Count; x++)
        {
            var row = Cells[x];
            for (var y = 0; y < row.Length; y++)
            {
                if (CanBeAccessedByAForklift(new Position(x, y)))
                {
                    result++;
                }
            }
        }

        return result;
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

