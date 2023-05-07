namespace IKApplication.MVC.ResultMessages
{
    public static class Messages
    {
        public static class User
        {
            public static string Create(string email)
            {
                return $"User {email} created successfully!";
            }

            public static string Update(string email)
            {
                return $"User {email} updated successfully!";
            }

            public static string Delete(string email)
            {
                return $"User {email} deleted successfully!";
            }
        }

        public static class Personal
        {
            public static string Create(string email)
            {
                return $"Personal {email} created successfully!";
            }

            public static string Update(string email)
            {
                return $"Personal {email} updated successfully!";
            }

            public static string Delete(string email)
            {
                return $"Personal {email} deleted successfully!";
            }
        }

        public static class CompanyAdmin
        {
            public static string Create(string email)
            {
                return $"Company admin {email} created successfully!";
            }

            public static string Update(string email)
            {
                return $"Company admin {email} updated successfully!";
            }

            public static string Delete(string email)
            {
                return $"Company admin {email} deleted successfully!";
            }
        }

        public static class SiteAdmin
        {
            public static string Create(string email)
            {
                return $"Site admin {email} created successfully!";
            }

            public static string Update(string email)
            {
                return $"Site admin {email} updated successfully!";
            }

            public static string Delete(string email)
            {
                return $"Site admin {email} deleted successfully!";
            }
        }

        public static class Company
        {
            public static string Create(string name)
            {
                return $"Company {name} created successfully!";
            }

            public static string Update(string name)
            {
                return $"Company {name} updated successfully!";
            }

            public static string Delete(string name)
            {
                return $"Company {name} deleted successfully!";
            }
        }


        public static class Register
        {
            public static string Success()
            {
                return $"Your registration application created successfully!";
            }
        }

        public static class Errors
        {
            public static string Error()
            {
                return $"An error occured!";
            }
        }
    }
}
