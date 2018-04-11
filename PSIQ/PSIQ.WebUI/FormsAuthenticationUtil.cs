using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Xml;

namespace PSIQ.WebUI
{
    public class FormsAuthenticationUtil
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="FormsAuthenticationUtil"/> class from being created.
        /// </summary>
        private FormsAuthenticationUtil()
        {
        }

        /// <summary>
        /// Creates a Forms authentication ticket and sets it within Uri or Cookie using the SetAuthCookieMain()
        /// private method, and redirects to the originally requested page 
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="commaSeperatedRoles">Comma seperated roles for the users</param>
        /// <param name="createPersistentCookie">True or false whether to create persistant cookie</param>
        /// <param name="strCookiePath">Path for which the authentication ticket is valid</param>
        private static void RedirectFromLoginPageMain(String userName, String commaSeperatedRoles, Boolean createPersistentCookie, String strCookiePath)
        {
            SetAuthCookieMain(userName, commaSeperatedRoles, createPersistentCookie, strCookiePath);
            HttpContext.Current.Response.Redirect(FormsAuthentication.GetRedirectUrl(userName, createPersistentCookie));
        }

        /// <summary>
        /// Creates Forms authentication ticket and redirects to the originally requested page. Uses the 
        /// RedirectFromLoginPageMain() private method
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="commaSeperatedRoles">Comma seperated roles for the users</param>
        /// <param name="createPersistentCookie">True or false whether to create persistant cookie</param>
        /// <param name="strCookiePath">Path for which the authentication ticket is valid</param>
        public static void RedirectFromLoginPage(String userName, String commaSeperatedRoles, Boolean createPersistentCookie, String strCookiePath)
        {
            RedirectFromLoginPageMain(userName, commaSeperatedRoles, createPersistentCookie, strCookiePath);
        }

        /// <summary>
        /// Creates Forms authentication ticket and redirects to the originally requested page. Uses the 
        /// RedirectFromLoginPageMain() private method
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="commaSeperatedRoles">Comma seperated roles for the users</param>
        /// <param name="createPersistentCookie">True or false whether to create persistant cookie</param>
        public static void RedirectFromLoginPage(String userName, String commaSeperatedRoles, Boolean createPersistentCookie)
        {
            RedirectFromLoginPageMain(userName, commaSeperatedRoles, createPersistentCookie, null);
        }

        /// <summary>
        /// Creates and returns the Forms authentication ticket 
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="commaSeperatedRoles">Comma seperated roles for the users</param>
        /// <param name="createPersistentCookie">True or false whether to create persistant cookie</param>
        /// <param name="strCookiePath">Path for which the authentication ticket is valid</param>
        private static FormsAuthenticationTicket CreateAuthenticationTicket(String userName, String commaSeperatedRoles, Boolean createPersistentCookie, String strCookiePath)
        {
            String cookiePath = strCookiePath == null ? FormsAuthentication.FormsCookiePath : strCookiePath;

            //Determine the cookie timeout value from web.config if specified
            int expirationMinutes = GetCookieTimeoutValue();

            //Create the authentication ticket
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
            1,                      //A dummy ticket version

            userName,               //User name for whome the ticket is issued

            DateTime.Now,           //Current date and time

            DateTime.Now.AddMinutes(expirationMinutes), //Expiration date and time

            createPersistentCookie, //Whether to persist coolkie on client side. If true, 
                                    //The authentication ticket will be issued for new sessions from
                                    //the same client PC    

            commaSeperatedRoles,    //Comma seperated user roles

            cookiePath);            //Path cookie valid for

            return ticket;
        }

        /// <summary>
        /// Creates the custom authentication ticket.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="userData">The user data.</param>
        /// <param name="createPersistentCookie">if set to <c>true</c> [create persistent cookie].</param>
        /// <returns></returns>
        private static FormsAuthenticationTicket CreateCustomAuthenticationTicket(String userName, String userData, Boolean createPersistentCookie)
        {
            var cookiePath = FormsAuthentication.FormsCookiePath;

            //Determine the cookie timeout value from web.config if specified
            var expirationMinutes = GetCookieTimeoutValue();
            var authTicket = new FormsAuthenticationTicket(
                     1,
                     userName,
                     DateTime.Now,
                     DateTime.Now.AddMinutes(expirationMinutes),
                     createPersistentCookie,
                     userData);

            return authTicket;
        }

        /// <summary>
        /// Retrieves cookie timeout value in the <forms></forms> section in the web.config file as this
        /// value is not accessable via the FormsAuthentication or any other built in class
        /// </summary>
        /// <returns></returns>
        private static int GetCookieTimeoutValue()
        {
            int timeout = 30; //Default timeout is 30 minutes
            var webConfig = new XmlDocument();
            webConfig.Load(HttpContext.Current.Server.MapPath(@"~\web.config"));
            XmlNode node = webConfig.SelectSingleNode("/configuration/system.web/authentication/forms");
            if (node != null && node.Attributes["timeout"] != null)
            {
                timeout = int.Parse(node.Attributes["timeout"].Value);
            }

            return timeout;
        }

        /// <summary>
        /// Sets the custom auth cookie.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="userData">The user data.</param>
        /// <param name="createPersistentCookie">if set to <c>true</c> [create persistent cookie].</param>
        public static void SetCustomAuthCookie(String userName, String userData, Boolean createPersistentCookie)
        {
            var ticket = CreateCustomAuthenticationTicket(userName, userData, createPersistentCookie);
            //Encrypt the authentication ticket
            var encrypetedTicket = FormsAuthentication.Encrypt(ticket);

            if (!FormsAuthentication.CookiesSupported)
            {
                //If the authentication ticket is specified not to use cookie, set it in the Uri
                FormsAuthentication.SetAuthCookie(encrypetedTicket, createPersistentCookie);
            }
            else
            {
                //If the authentication ticket is specified to use a cookie, wrap it within a cookie.
                //The default cookie name is .ASPXAUTH if not specified 
                //in the <forms> element in web.config
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypetedTicket);

                //Set the cookie's expiration time to the tickets expiration time
                if (ticket.IsPersistent)
                {
                    authCookie.Expires = ticket.Expiration;
                }
                ////Set the cookie in the Response
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }

        /// <summary>
        /// Creates a Forms authentication ticket and writes it in Url or embeds it within Cookie. Uses the             
        /// SetAuthCookieMain() private method 
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="commaSeperatedRoles">Comma seperated roles for the users</param>
        /// <param name="createPersistentCookie">True or false whether to create persistant cookie</param>
        public static void SetAuthCookie(String userName, String commaSeperatedRoles, Boolean createPersistentCookie)
        {
            SetAuthCookieMain(userName, commaSeperatedRoles, createPersistentCookie, null);
        }

        /// <summary>
        /// Creates a Forms authentication ticket and writes it in Url or embeds it within Cookie. Uses the             
        /// SetAuthCookieMain() private method 
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="commaSeperatedRoles">Comma seperated roles for the users</param>
        /// <param name="createPersistentCookie">True or false whether to create persistant cookie</param>
        /// <param name="strCookiePath">Path for which the authentication ticket is valid</param>
        public static void SetAuthCookie(String userName, String commaSeperatedRoles, Boolean createPersistentCookie, String strCookiePath)
        {
            SetAuthCookieMain(userName, commaSeperatedRoles, createPersistentCookie, strCookiePath);
        }

        /// <summary>
        /// Creates Forms authentication ticket using the private method CreateAuthenticationTicket() and writes 
        /// it in Url or embeds it within Cookie 
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="commaSeperatedRoles">Comma seperated roles for the users</param>
        /// <param name="createPersistentCookie">True or false whether to create persistant cookie</param>
        /// <param name="strCookiePath">Path for which the authentication ticket is valid</param>
        private static void SetAuthCookieMain(String userName, String commaSeperatedRoles, Boolean createPersistentCookie, String strCookiePath)
        {
            FormsAuthenticationTicket ticket = CreateAuthenticationTicket(userName, commaSeperatedRoles, createPersistentCookie, strCookiePath);
            //Encrypt the authentication ticket
            String encrypetedTicket = FormsAuthentication.Encrypt(ticket);

            if (!FormsAuthentication.CookiesSupported)
            {
                //If the authentication ticket is specified not to use cookie, set it in the Uri
                FormsAuthentication.SetAuthCookie(encrypetedTicket, createPersistentCookie);
            }
            else
            {
                //If the authentication ticket is specified to use a cookie, wrap it within a cookie.
                //The default cookie name is .ASPXAUTH if not specified 
                //in the <forms> element in web.config
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypetedTicket);

                //Set the cookie's expiration time to the tickets expiration time
                if (ticket.IsPersistent) authCookie.Expires = ticket.Expiration;
                ////Set the cookie in the Response
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }

        /// <summary>
        /// Adds roles to the current User in HttpContext after forms authentication authenticates the user
        /// so that, the authorization mechanism can authorize user based on the groups/roles of the user
        /// </summary>
        public static void AttachRolesToUser()
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;

                        FormsAuthenticationTicket ticket = (id.Ticket);

                        if (!FormsAuthentication.CookiesSupported)
                        {
                            //If cookie is not supported for forms authentication, then the 
                            //authentication ticket is stored in the Url, which is encrypted.
                            //So, decrypt it
                            ticket = FormsAuthentication.Decrypt(id.Ticket.Name);
                        }

                        // Get the stored user-data, in this case, user roles
                        if (!String.IsNullOrEmpty(ticket.UserData))
                        {
                            String userData = ticket.UserData;

                            String[] roles = userData.Split(',');
                            //Roles were put in the UserData property in the authentication ticket
                            //while creating it

                            HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(id, roles);
                        }
                    }
                }
            }
        }

        public static CustomPrincipalSerializeModel UserAuthenticated
        {
            get
            {
                if (HttpContext.Current.User == null || !HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return null;
                }

                if (HttpContext.Current.User.Identity is FormsIdentity)
                {
                    var identity = (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = (identity.Ticket);
                    if (!FormsAuthentication.CookiesSupported)
                    {
                        ticket = FormsAuthentication.Decrypt(identity.Ticket.Name);
                    }

                    if (!string.IsNullOrEmpty(ticket.UserData))
                    {
                        return new JavaScriptSerializer().Deserialize<CustomPrincipalSerializeModel>(ticket.UserData);
                    }
                }

                HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null)
                {
                    var ticket = FormsAuthentication.Decrypt(cookie.Value);
                    return new JavaScriptSerializer().Deserialize<CustomPrincipalSerializeModel>(ticket.UserData);
                }

                return null;
            }
        }

        /// <summary>
        /// Sign out.
        /// </summary>
        public static void SignOut()
        {
            FormsAuthentication.SignOut();

            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.RemoveAll();

            var cookieName = new HttpCookie(FormsAuthentication.FormsCookieName, "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            HttpContext.Current.Response.Cookies.Add(cookieName);

            var cookieAspNet = new HttpCookie("ASP.NET_SessionId", "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            HttpContext.Current.Response.Cookies.Add(cookieAspNet);
        }
    }
}