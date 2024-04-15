namespace PassIn.Exceptions;

// ' : ' -> creates inheritance in C#, so that this CLASS is seen as an Exception(Error)
public class PassInException : SystemException
{
// ' : ' -> It takes the message and send for the SystemException constructor
    public PassInException(string message) : base(message)
    {
        
    }
}