using WebUniDiary.Models;

namespace WebUniDiary.Controllers
{
    public class StudentController
    {
        private Student _user;
        bool userIsSetUp = false;

        public StudentController(Student user)
        {
            _user = user;
            userIsSetUp = true;
        }

        public StudentController()
        {
            _user = new Student();
            userIsSetUp = false;
        }

        public bool setUserID(int id)
        {
            _user.UserId = id;
            return true;
        }

        public bool setFacultyNumber(string id)
        {
            if (id.Length < 6)
            {
                return false;
            }

            _user.FacultyNumber = id;

            return true;
        }

        public bool setFirstName(string firstName)
        {
            _user.FirstName = firstName;
            return true;
        }

        public bool setLastName(string lastName)
        {
            _user.LastName = lastName;
            return true;
        }

        public bool setSpecialty(string specialty)
        {
            //TODO filter if the Specialty is valid, add a static Valido Class
            string searchString = "banana";

            if (specialty.Contains(searchString))
            {
                return false;
            }

            _user.Specialty = specialty;
            return true;
        }

        public bool setEGN(string egn)
        {
            if (egn.Length != 10)
            {
                return false;
            }

            _user.EGN = egn;
            return true;
        }

        public bool setPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length != 10)
            {
                return false;
            }

            _user.PhoneNumber = phoneNumber;
            return true;
        }

        public bool setAddress(string address)
        {
            _user.Address = address;
            return true;
        }

        public bool setActive(bool active)
        {
            _user.Active = active;
            return true;
        }

        public Student getUser()
        {
            return _user;
        }
    }
}
