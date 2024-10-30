using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext.Abstractions;
using Balta.Domain.SharedContext.Extensions;
using Balta.Domain.Test.AccountContext.ValueObjects.MockObjects;
using Xunit.Sdk;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class EmailTests
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public EmailTests()
    {
        _dateTimeProvider = new MockDateTimeProvider();
    }
        
    [Fact]
    public void ShouldLowerCaseEmail()
    {
        //arrange
        var testAddress = "EMAILtest@gmail.com";        
        var expectedAddress = testAddress.ToLower();

        //act
        var email = Email.ShouldCreate(testAddress, _dateTimeProvider);
        var actualAddress = email.Address;

        //assert
        Assert.Equal(expectedAddress, actualAddress);
    }
    
    [Fact]
    public void ShouldTrimEmail()
    {
        //arrange
        var testAddress = "emailtest@gmail.com  ";
        var expectedAddress = testAddress.Trim();

        //act
        var email = Email.ShouldCreate(testAddress, _dateTimeProvider);
        var actualAddress = email.Address;

        //assert
        Assert.Equal(expectedAddress, actualAddress);
    }
    
    [Fact]
    public void ShouldFailIfEmailIsNull()
    {
        //arrange
        string? testAddress = null;

        //act

        //assert
        Assert.Throws<NullReferenceException>( () =>
        {
            var email = Email.ShouldCreate(testAddress, _dateTimeProvider);
        });
    }
    
    [Fact]
    public void ShouldFailIfEmailIsEmpty()
    {
        //arrange
        string testAddress = string.Empty;

        //act

        //assert
        Assert.Throws<InvalidEmailException>(() =>
        {
            var email = Email.ShouldCreate(testAddress, _dateTimeProvider);
        });
    }
    
    [Fact]
    public void ShouldFailIfEmailIsInvalid()
    {
        //arrange
        string testAddress = "teste123.com";

        //act

        //assert
        Assert.Throws<InvalidEmailException>(() =>
        {
            var email = Email.ShouldCreate(testAddress, _dateTimeProvider);
        });
    }
    
    [Fact]
    public void ShouldPassIfEmailIsValid()
    {
        //arrange
        var testValidEmailAddress = "emailtest@gmail.com";

        //act
        var email = Email.ShouldCreate(testValidEmailAddress, _dateTimeProvider);

        //assert
        Assert.True(testValidEmailAddress == email.Address);
    }
    
    [Fact]
    public void ShouldHashEmailAddress()
    {
        //arrange
        var testValidEmailAddress = "emailtest@gmail.com";
        var expectedHash = testValidEmailAddress.ToBase64();

        //act
        var email = Email.ShouldCreate(testValidEmailAddress, _dateTimeProvider);
        var actualHash = email.Hash;

        //assert
        Assert.False(string.IsNullOrEmpty(actualHash)); 
        Assert.Equal(expectedHash, actualHash);
    }
    
    [Fact]
    public void ShouldExplicitConvertFromString()
    {
        Assert.Fail();
    }
    
    [Fact]
    public void ShouldExplicitConvertToString()
    {
        //arrange
        var expectedEmailAddress = "emailtest@gmail.com";

        //act
        var email = Email.ShouldCreate(expectedEmailAddress, _dateTimeProvider);
        var emailConverted = (string) email;
        var emailAddress = email.Address;

        //assert
        Assert.Equal(expectedEmailAddress, emailConverted);
        Assert.Equal(emailAddress, emailConverted);
    }
    
    [Fact]
    public void ShouldReturnEmailWhenCallToStringMethod()
    {
        //arrange
        var expectedEmailAddress = "emailtest@gmail.com";

        //act
        var email = Email.ShouldCreate(expectedEmailAddress, _dateTimeProvider);
        var emailFromToString = email.ToString();
        var emailAddress = email.Address;

        //assert
        Assert.Equal(expectedEmailAddress, emailFromToString);
        Assert.Equal(emailAddress, emailFromToString);
    }
}