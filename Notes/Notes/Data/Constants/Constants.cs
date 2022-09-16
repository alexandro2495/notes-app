using System;
namespace Notes.Data.Constants
{
    public class Constants
    {
        //Dialog Messages
        public const string DIALOG_QUESTION = "Do you want to continue?";
        public const string DIALOG_DELETE_NOTE = "You are going to delete this note: ";
        public const string DIALOG_DELETE_NOTES = "Your selection is going to be delete it";
        public const string DIALOG_AUTHENTICATION_LOGOUT = "Your about to log out.";

        //Default Messages
        public const string OK = "OK";
        public const string YES = "YES";
        public const string NO = "NO";
        //Error Messages
        public const string ERRMSG_DELETE_NOTE = "Ups!";
        public const string ERRMSG_DELETE_NOTE_DESC = "We weren't able to perform the delete action, please try again later.";
        public const string ERRMSG_AUTHENTICATION = "Bad Credentials!";
        public const string ERRMSG_AUTHENTICATION_SIGN_IN = "Sign In failed!";
        public const string ERRMSG_AUTHENTICATION_SIGN_UP = "Sign Up failed!";
        public const string ERRMSG_AUTHENTICATION_LOGOUT = "Log Out failed!";
        public const string ERRMSG_AUTHENTICATION_LOGOUT_DESC = "we weren't able to perform log out, please try again later.";
        // MessagingCenter constants
        public const string MSGC_SET_ROOT_PAGE = "SET_ROOT_PAGE";
        public const string MSGC_NEW_NOTE = "NEW_NOTE";
        public const string MSGC_UPDATE_NOTE = "UPDATE_NOTE";

        // Pages Titles
        public const string NotePageTitle = "My Notes";
        public const string NoteDetailPageTitle = "Note Detail";
        public const string NoteAddPageTitle = "New Note";
        public const string LoginPageTitle = "Login";
        public const string SignUpPageTitle = "Sign Up";

        //default colors AppConfiguration
        public const string PrimaryColor = "205375";
        public const string DarkColor = "112B3C";
        public const string AccentColor = "F66B0E";
        public const string PageBackgroundColor = "EFEFEF";
        public const string FontFamily = "";
        public const string Language = "ES";

    }
}

