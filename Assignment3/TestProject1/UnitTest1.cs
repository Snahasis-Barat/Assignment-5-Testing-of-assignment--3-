using Assignment3;
using Assignment3.Controllers;
using Assignment3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Xml.Linq;

namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
        private static DbContextOptions<StudentDbContext> options = new DbContextOptionsBuilder<StudentDbContext>().UseSqlServer(@"Server=DESKTOP-27ECLDH;Database=WebAPI;Trusted_Connection=True").Options;
        //private Student st1;



        
        
        
      [Test]

        public void CreateStudentsTest()
        {
            Student s2 = new Student();
            
            /*s2.name = "Snahasis";
            s2.address = "Kolkata";
            s2.standard = "XII";
            s2.age= 18;*/
             
            using(var context=new StudentDbContext(options)) { 
            
            var repository=new StudentController(context);
                
                
                    var repository2 = repository.CreateStudent(s2);
                    var result = repository2.Result as OkObjectResult;
                    Assert.AreEqual(200, result.StatusCode);
               
            }
            
            
        }
        
      //  [Test]

        public void GetStudentsTest()
        {
            using (var context = new StudentDbContext(options))
            {
              

                var repository = new StudentController(context);
               List<Student>repository2= repository.GetStudents();
                Assert.AreEqual(1, repository2.Count);

            }
        }

      //  [Test]

        public void GetStudentById()
        {
            Student s2 = new Student();
            s2.Id = 10;
            s2.name = "Snahasis Barat";
            s2.address = "Hyderabad";
            s2.age = 30;
            s2.standard = "XII";
            using (var context = new StudentDbContext(options))
            {
                var repository = new StudentController(context);
               repository.GetStudentById(11);
                

            }
            using (var context = new StudentDbContext(options))
            {
                var repository = context.Students.FirstOrDefault(x => x.Id == 10);
                Assert.AreEqual(s2.Id, repository.Id);
                Assert.AreEqual(s2.name, repository.name);
                Assert.AreEqual(s2.address, repository.address);
                Assert.AreEqual(s2.standard, repository.standard);
                Assert.AreEqual(s2.age, repository.age);
            }
        }
       //[Test]
       public void DeleteStudentTest()
        {
            using (var context = new StudentDbContext(options))
            {
                var repository = new StudentController(context);
               var repository2= repository.DeleteStudent(22);

                var result = repository2.Result as OkObjectResult ;
                Assert.AreEqual(200,result.StatusCode);
            }
            

        }
        
      //  [Test]
        public void UpdateStudentTest()
        {
            Student s2 = new Student();

            s2.name = "Snahasis Barat";
            s2.address = "Noida";
            s2.age = 30;
            s2.standard = "XII";
            using (var context = new StudentDbContext(options))
            {
                var repository = new StudentController(context);
                var repository2 = repository.UpdateStudent(10, s2);
                var result = repository2.Result as OkObjectResult;
                Assert.AreEqual(200, result.StatusCode);
            }
        }
    }
}