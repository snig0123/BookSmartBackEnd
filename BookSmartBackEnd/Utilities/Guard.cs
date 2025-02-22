namespace BookSmartBackEnd.Utilities;

public class Guard
{
    public static void IsNotNull(object argumentValue, string argumentName)
    {
        if(argumentValue == null)
            throw new ArgumentNullException(argumentName);
    }
}