using Toarnbeike.Eithers.Collections;
using Toarnbeike.Eithers.TestExtensions;

namespace Toarnbeike.Eithers.Collection;

public class CollectionExtensionsTests
{
    private readonly IEnumerable<Either<string, int>> _allRights;
    private readonly IEnumerable<Either<string, int>> _mixed;
    private readonly IEnumerable<Either<string, int>> _allLefts;
    private readonly IEnumerable<int> _ints = [1,2,3];

    public CollectionExtensionsTests()
    {
        var leftA =  Either<string, int>.Left("A");
        var leftB =  Either<string, int>.Left("B");
        
        _allRights = [1, 2, 3];
        _mixed = [1, 2, leftA, 3, leftB];
        _allLefts = [leftA, leftB];
    }

    [Fact]
    public void Traverse_Should_Throw_WhenInputIsNull()
    {
        IEnumerable<int>? nullCollection = null;
        Should.Throw<ArgumentNullException>(() => nullCollection!.Traverse(Either<string, int>.Right));
    }

    [Fact]
    public void Traverse_Should_CreateRightSequence_WhenAllRight()
    {
        var traversed = _ints.Traverse((Func<int, Either<string, int>>)Bind);
        
        traversed.ShouldBeOfType<Either<string, IEnumerable<int>>>();
        var actual = traversed.ShouldBeRight();
        actual.ShouldBe(_ints.Select(value => value * 2));
        return;

        Either<string, int> Bind(int value) => 
            Either<string, int>.Right(value * 2);
    }

    [Fact]
    public void Traverse_Should_CreateLeftValue_WhenItemIsLeft()
    {
        var traversed = _ints.Traverse(Bind);
        
        traversed.ShouldBeOfType<Either<string, IEnumerable<int>>>();
        var actual = traversed.ShouldBeLeft();
        actual.ShouldBe("too high");
        return;

        Either<string, int> Bind(int value) => 
            value < 3 ? Either<string, int>.Right(value * 2) : Either<string, int>.Left("too high");
    }

    [Fact]
    public async Task TraverseAsync_Should_CreateRightSequence_WhenAllRight()
    {
        var traversed = await _ints.TraverseAsync(BindAsync);

        traversed.ShouldBeOfType<Either<string, IEnumerable<int>>>();
        var actual = traversed.ShouldBeRight();
        actual.ShouldBe(_ints.Select(value => value * 2));
        return;
        
        Task<Either<string, int>> BindAsync(int value) => Task.FromResult(Either<string, int>.Right(value * 2));
    }

    [Fact]
    public async Task TraverseAsync_Should_CreateLeftValue_WhenItemIsLeft()
    {
        var traversed = await _ints.TraverseAsync(BindAsync);
        
        traversed.ShouldBeOfType<Either<string, IEnumerable<int>>>();
        var actual = traversed.ShouldBeLeft();
        actual.ShouldBe("too high");
        return;
        
        Task<Either<string, int>> BindAsync(int value) => 
            value < 3 ? Task.FromResult(Either<string, int>.Right(value * 2)) : Task.FromResult(Either<string, int>.Left("too high"));
    }
    
    [Fact]
    public void Sequence_Should_CreateRightSequence_WhenAllRight()
    {
        var actual = _allRights.Sequence().ShouldBeRight();
        actual.ShouldBe([1,2,3]);
    }
    
    [Fact]
    public void Sequence_Should_CreateLeftValue_WhenItemIsLeft()
    {
        _mixed.Sequence().ShouldBeLeftWithValue("A");
    }

    [Fact]
    public void TapAll_Should_ApplyToAllRightValues()
    {
        var sum = 0;
        _allRights.TapAll(Increase);
        sum.ShouldBe(6);
        return;

        void Increase(int value) => sum += value;
    }

    [Fact]
    public void TapAll_Should_WorkAlsoOnMixedCollection()
    {
        var sum = 0;
        _mixed.TapAll(Increase);
        sum.ShouldBe(6);
        return;

        void Increase(int value) => sum += value;
    }
    
    [Fact]
    public void AnyLeft_Should_ReturnTrue_WhenLeft()
    {
        _mixed.AnyLeft().ShouldBeTrue();
    }
    
    [Fact]
    public void AnyLeft_Should_ReturnFalse_WhenRight()
    {
        _allRights.AnyLeft().ShouldBeFalse();
    }
    
    [Fact]
    public void AnyRight_Should_ReturnTrue_WhenRight()
    {
        _mixed.AnyRight().ShouldBeTrue();
    }
    
    [Fact]
    public void AnyRight_Should_ReturnFalse_WhenLeft()
    {
        _allLefts.AnyRight().ShouldBeFalse();
    }
    
    [Fact]
    public void CountLeft_Should_ReturnCountOfLefts()
    {
        _mixed.CountLeft().ShouldBe(2);
        _allRights.CountLeft().ShouldBe(0);
    }
    
    [Fact]
    public void CountRight_Should_ReturnCountOfRights()
    {
        _mixed.CountRight().ShouldBe(3);
        _allLefts.CountRight().ShouldBe(0);
    }

    [Fact]
    public void Lefts_Should_ReturnLefts()
    {
        var result = _mixed.Lefts();
        result.ShouldBe(["A", "B"]);
    }
    
    [Fact]
    public void Rights_Should_ReturnRights()
    {
        var result = _mixed.Rights();
        result.ShouldBe([1,2,3]);
    }

    [Fact]
    public void Partition_Should_ReturnPartition()
    {
        var (lefts, rights) = _mixed.Partition();
        lefts.ShouldBe(["A", "B"]);
        rights.ShouldBe([1,2,3]);
    }
}