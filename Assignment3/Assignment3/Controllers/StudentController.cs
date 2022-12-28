using Assignment3.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Assignment3.Controllers
{

    [Route("api/Student")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly StudentDbContext _db;
        public StudentController(StudentDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public ActionResult<Student> CreateStudent([FromBody] Student s)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try 
            {
                _db.Students.Add(s);
                _db.SaveChanges();
                    return Ok(s);
                }
            catch { 
                return BadRequest("All student details are required");
            }
        }

        [HttpGet]
        [Route("GetStudents")]
        public List<Student> GetStudents()
        {
            
            
            return _db.Students.ToList();
        }

        [HttpGet("id")]
        public ActionResult<Student> GetStudentById(int id) {

            try
            {
                Student s = _db.Students.FirstOrDefault(x => x.Id == id);

                return Ok(s);
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid id");
            }
            
             
        }

        [HttpPut("id")]
            public ActionResult<Student> UpdateStudent(int id,[FromBody] Student s) {

            try
            {
                Student st = _db.Students.FirstOrDefault(x => x.Id == id);
                st.standard = s.standard;
                st.name = s.name;
                st.address = s.address;
                st.age = s.age;
                _db.SaveChanges();
                return Ok(st);
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid id");
            }


            }

        [HttpPatch("id")]
        public ActionResult<Student> UpdatePartialStudent(int id,JsonPatchDocument<Student> s) {
            try
            {
                Student stu = _db.Students.FirstOrDefault(x => x.Id == id);
                s.ApplyTo(stu, ModelState);
                _db.SaveChanges();
                return Ok(s);
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid student id");
            }
        
        
        }

        [HttpDelete("id")]
        public ActionResult<Student> DeleteStudent(int id)
        {
            try
            {
                var s = _db.Students.FirstOrDefault(x => x.Id == id);
                _db.Students.Remove(s);
                _db.SaveChanges();
                return Ok("Deleted successfully");
            }
                catch(Exception ex)
            {
                return BadRequest("Invalid id");
            }
           
           
        }
    }
}
