namespace Microservice.Business
{
    public class Constants
    {
             // AUTHENTICATION
             public const string InvalidUsername = "Incorrect login details.";
             public const string InvalidPassword = "Incorrect login details.";
             public const string SessionHeaderName = "X-SESSION-ID";
             public const string AccessTokenHeaderName = "X-TOKEN";
             public const string RefreshTokenHeaderName = "X-REFRESH";
             public const string AccountLockedOutMessage = "Account locked out. Try again later.";
             public const string InvalidToken = "Invalid Token";
             public const string FailedAuthentication = "Authentication was failed";
             public const string InvalidOneTimeUrl = "Invalid Authorization Url";
     
             // REGISTRATION
             public const string EmptyUsernameOrPassword = "Incorrect login details.";
             public const string InvalidEmailAddressFormat = "Incorrect e-mail address";
             public const string EmailAlreadyInUse = "Incorrect e-mail address";
             public const string PasswordNotMeetComplexity = "Password must be a minimum of 8 characters and contain at least 1 x Uppercase letter and 1 x Lowercase letter, and at least 1 x Symbol or Digit.";
             public const string EmptyUsernameOrPasswordInRegister = "Invalid Username or Password";
             public const string EmptyUserIdentifier = "Invalid user identifier";
             
             // PASSWORD POLICY
             public const int PasswordMinimumLength = 8;
             public const int PasswordMinimumUpperCase = 1;
             public const int PasswordMinimumLowerCase = 1;
             public const int PasswordMinimumNonAlpha = 1;
             public const int PasswordMinimumNumeric = 1;
             
             // ASSESSMENTS
             public const string DefaultLanguage = "EN-US";
             public const string InvalidAssessmentType = "Invalid Assesment Type";
             public const string InvalidProductName = "Invalid Product Name";
             public const string ProductTextNotFound = "No Product Text Found";
             public const string InvalidAssessmentName = "Invalid Assessment Name";
             public const string InvalidQuestionnaireName = "Invalid Questionnaire Name";
             public const string InvalidOrderIdentifier = "Invalid Order Identifier";
             public const string InvalidQuestionnaireType = "Invalid Questionnaire Type";
             public const string QuestionnaireNotFound = "No Questionnaire Found";
             public const string DuplicatedOrganisationMatched = "More than one organisation matched with organisation name";
             public const string InvalidOrganisationName = "Invalid organisation name";
             public const string InvalidDueDate = "Invalid due date";
     
             // USER
             public const string CreateUserFailed = "Unable to create user";
             public const string InvalidUserIdentifier = "Invalid User Identifier";
             
             // PRIVACY POLICY
             public const string PolicyIdentifierNotExist = "Policy Identifier does not exist";
             public const string PrivacyPolicyNotExist = "Privacy policy does not exist";
     
             // SURVEY EXPIRY
             public const string ActualSurveyType = "QUESTIONNAIRE";
             public const string CompletedByUser = "User";
             public const string CompletedBySystem = "System";
             public const string SurveyIsComplete = "The survey has already been completed";
             public const int SurveySubmitAllowance = 60;   
    }
}