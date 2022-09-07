using System;
namespace Notes.Data.Constants
{
    public class Constants
    {
        //Default Messages
        public const string OK = "OK";
        //Error Messages
        public const string ERRMSG_AUTHENTICATION = "Bad Credentials!";
        public const string ERRMSG_AUTHENTICATION_SIGN_UP = "Sign up failed!";
        public const string ERRMSG_AUTHENTICATION_LOGOUT = "LogOut failed!";
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

    public enum RootPage
    {
        None = 0,
        LoginPage = 1,
        NotePage = 2,
        NoteCollectionPage = 3
    }
}

