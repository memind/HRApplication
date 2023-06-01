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

            public static string Accept(string mail)
            {
                return $"User {mail}'s request accepted successfully!";
            }
        }

        public static class CompanyAdminAndPersonal
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

            public static string Accept(string name)
            {
                return $"Company {name}'s request accepted successfully!";
            }
        }

        public static class Register
        {
            public static string Success()
            {
                return $"Your registration application created successfully!";
            }
        }

        public static class ResetPasswordMessage
        {
            public static string Success()
            {
                return $"Your password changed successfully!";
            }
            public static string Set()
            {
                return $"Your password settled successfully!";
            }
            public static string Error()
            {
                return $"The email address you entered is not registered!";
            }
        }

        public static class Title
        {
            public static string Create()
            {
                return $"Title created successfully!";
            }
            public static string Update()
            {
                return $"Title updated successfully!";
            }
            public static string Delete()
            {
                return $"Title deleted successfully!";
            }
            public static string Recover()
            {
                return $"Title recovered successfully!";
            }
        }

        public static class Profession
        {
            public static string Create()
            {
                return $"Profession created successfully!";
            }
            public static string Update()
            {
                return $"Profession updated successfully!";
            }
            public static string Delete()
            {
                return $"Profession deleted successfully!";
            }
            public static string Recover()
            {
                return $"Profession recovered successfully!";
            }
        }

        public static class Expense
        {
            public static string Create()
            {
                return $"Expense created successfully! The request email has been sent to your patron!";
            }
            public static string Update()
            {
                return $"Expense updated successfully!";
            }
            public static string Delete()
            {
                return $"Expense deleted successfully!";
            }
            public static string Accept(string description)
            {
                return $"Expense {description} accepted successfully!";
            }
            public static string Refuse(string description)
            {
                return $"Expense {description} refused successfully!";
            }
        }

        public static class Advance
        {
            public static string Create()
            {
                return $"Advance created successfully! The request email has been sent to your patron!";
            }
            public static string Update()
            {
                return $"Advance updated successfully!";
            }
            public static string Delete()
            {
                return $"Advance deleted successfully!";
            }
            public static string Accept(string fullname)
            {
                return $"{fullname}'s advance request accepted successfully!";
            }
            public static string Refuse(string fullname)
            {
                return $"{fullname}'s advance request refused successfully!";
            }
        }

        public static class Leaves
        {
            public static string Create()
            {
                return $"Leave created successfully! The request email has been sent to your patron!";
            }
            public static string Update()
            {
                return $"Leave updated successfully!";
            }
            public static string Delete()
            {
                return $"Leave deleted successfully!";
            }
            public static string Accept()
            {
                return $"Leave accepted successfully!";
            }
            public static string Refuse()
            {
                return $"Leave refused successfully!";
            }
            public static string Cannot()
            {
                return $"You cannot add more leave. Because you have already reached 20 days!";
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
