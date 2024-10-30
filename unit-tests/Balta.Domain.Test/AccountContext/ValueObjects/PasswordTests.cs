using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class PasswordTests
{
    [Fact]
    public void ShouldFailIfPasswordIsNull()
    {
        //arrange
        string? testPassword = null;

        //act
        //assert
        Assert.Throws<InvalidPasswordException>(() =>
        {
            Password password = Password.ShouldCreate(testPassword);
        });
        
    }
    
    [Fact]
    public void ShouldFailIfPasswordIsEmpty()
    {
        //arrange
        string testPassword = string.Empty;

        //act
        //assert
        Assert.Throws<InvalidPasswordException>(() =>
        {
            Password password = Password.ShouldCreate(testPassword);
        });
    }
    
    [Fact]
    public void ShouldFailIfPasswordIsWhiteSpace()
    {
        //arrange
        string testPassword = " ";

        //act
        //assert
        Assert.Throws<InvalidPasswordException>(() =>
        {
            Password password = Password.ShouldCreate(testPassword);
        });
    }
    
    [Fact]
    public void ShouldFailIfPasswordLenIsLessThanMinimumChars()
    {
        //arrange
        string testPassword = "123";
        int actualMinLength = 8;

        //act

        //assert
        Assert.Throws<InvalidPasswordException>(() =>
        {
            Password password = Password.ShouldCreate(testPassword);
        });
        Assert.True(testPassword.Length < actualMinLength);
    }
    
    [Fact]
    public void ShouldFailIfPasswordLenIsGreaterThanMaxChars()
    {
        //arrange
        string testPassword = string.Join(',',new string[50]) ;
        int actualMaxLength = 48;

        //act

        //assert
        Assert.Throws<InvalidPasswordException>(() =>
        {
            Password password = Password.ShouldCreate(testPassword);
        });
        Assert.True(testPassword.Length > actualMaxLength);
    }
    
    [Fact]
    public void ShouldHashPassword()
    {
        //arrange
        var testPassword = "Balt@desf1o";

        //act
        Password password = Password.ShouldCreate(testPassword);

        //assert
        Assert.False(string.IsNullOrEmpty(password.Hash));
    }
    
    [Fact]
    public void ShouldVerifyPasswordHash()
    {
        //arrange
        var testPassword = "Balt@desf1o";
        Password password = Password.ShouldCreate(testPassword);
        var hash = password.Hash;

        //act
        bool IsMatched = Password.ShouldMatch(hash, testPassword);

        //assert
        Assert.True(IsMatched);
    }

    [Fact]
    public void ShouldGenerateStrongPassword()
    {
        //arrange        
        string password = Password.ShouldGenerate();
        
        //act

        //assert
        Assert.False(string.IsNullOrEmpty(password));
        Assert.False(string.IsNullOrWhiteSpace(password));
    }
    
    [Fact]
    public void ShouldImplicitConvertToString()
    {
        //arrange
        var testPassword = "Balt@desf1o";
        string testImplicitPassword = "";
        string testImplicitPassword2 = "";

        //act
        Password password = Password.ShouldCreate(testPassword);

        testImplicitPassword = (string)password;
        testImplicitPassword2 = (Password)"teste";

        //assert
        Assert.False(string.IsNullOrEmpty(testImplicitPassword2));
        Assert.False(string.IsNullOrWhiteSpace(testImplicitPassword2));
        Assert.False(string.IsNullOrEmpty(testImplicitPassword));
        Assert.False(string.IsNullOrWhiteSpace(testImplicitPassword));
    }
    
    [Fact]
    public void ShouldReturnHashAsStringWhenCallToStringMethod()
    {
        //arrange
        var testPassword = "Balt@desf1o";
        string testHash = "";

        //act
        Password password = Password.ShouldCreate(testPassword);
        string hash = password.Hash;
        testHash = password.ToString();

        //assert
        Assert.False(string.IsNullOrEmpty(testHash));
        Assert.False(string.IsNullOrWhiteSpace(testHash));
        Assert.Equal(testHash, hash);
        
    }
    
    [Fact]
    public void ShouldMarkPasswordAsExpired() => Assert.Fail();
    
    [Fact]
    public void ShouldFailIfPasswordIsExpired() => Assert.Fail();
    
    [Fact]
    public void ShouldMarkPasswordAsMustChange() => Assert.Fail();
    
    [Fact]
    public void ShouldFailIfPasswordIsMarkedAsMustChange() => Assert.Fail();
}