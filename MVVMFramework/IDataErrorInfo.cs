namespace System.ComponentModel
{
    //
    // Summary:
    //     Provides the functionality to offer custom error information that a user interface
    //     can bind to.
    public interface IDataErrorInfo
    {
        //
        // Summary:
        //     Gets the error message for the property with the given name.
        //
        // Parameters:
        //   columnName:
        //     The name of the property whose error message to get.
        //
        // Returns:
        //     The error message for the property. The default is an empty string ("").
        string this[string columnName] { get; }

        //
        // Summary:
        //     Gets an error message indicating what is wrong with this object.
        //
        // Returns:
        //     An error message indicating what is wrong with this object. The default is an
        //     empty string ("").
        string Error { get; }
    }
}