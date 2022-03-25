using CRUD_WEB_API.DbContexts;
using CRUD_WEB_API.DTO;
using CRUD_WEB_API.Helper;
using CRUD_WEB_API.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_WEB_API.Repository
{
    public class Student : IStudent
    {


        private readonly DbContextCom _dbContext;

        public Student(DbContextCom dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<MessageHelper> CreateStudent(StudentDTO create)
        {
            try
            {
                var check = _dbContext.TblStudents.Where(x => x.StrPhoneNo == create.PhoneNo).FirstOrDefault();

                if (check != null)
                    throw new Exception("Student with Phone No [ " + create.PhoneNo + " ] is Already Exist");

                var entity = new Models.TblStudent
                {
                    StrStudentName = create.StudentName,
                    StrPhoneNo = create.PhoneNo,
                    StrAddress = create.Address,
                    StrBloodGroup = create.BloodGroup,
                    InsertDateTime = DateTime.Now,

                };

                await _dbContext.TblStudents.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                return new MessageHelper
                {
                    statuscode = 200,
                    Message = "Create Successfully"
                };
            }
            catch (Exception)
            {


                throw new NotImplementedException();
            }
        }

            public async Task<MessageHelper> EditStudent(StudentDTO edit)
            {

                try
                {
                    var update = _dbContext.TblStudents.Where(x => x.IntStudentId == edit.StudentId).FirstOrDefault();

                    if (update == null)
                        throw new Exception("Student Update Data Not Found");

                    update.StrStudentName = edit.StudentName;
                    update.StrAddress = edit.Address;
                    update.StrPhoneNo = edit.PhoneNo;
                    update.StrBloodGroup = edit.BloodGroup;

                    _dbContext.TblStudents.Update(update);
                    await _dbContext.SaveChangesAsync();

                    return new MessageHelper
                    {
                        Message = "Edited Successfully",
                        statuscode = 200
                    };
                }
             

                catch (Exception)
                {
                throw new NotImplementedException();

            }
               
            
        }

        public async Task<StudentDTO> GetStudentById(long studentId)
        {

            try
            {
                var data = await Task.FromResult((from a in _dbContext.TblStudents
                                                  where a.IntStudentId == studentId

                                                  select new StudentDTO
                                                  {
                                                      StudentId = a.IntStudentId,
                                                      StudentName = a.StrStudentName,
                                                      PhoneNo = a.StrPhoneNo,
                                                      Address = a.StrAddress,
                                                      BloodGroup = a.StrBloodGroup,
                                                     
                                                  }).FirstOrDefault());

                if (data == null)
                    throw new Exception("Student Data Not Found");

                return data;
            }
            catch (Exception)
            {

                throw;
            }
            
            throw new NotImplementedException();
        }

        public async Task<List<StudentDTO>> GetStudentList()
        {

            try
            {
                var data = await Task.FromResult((from a in _dbContext.TblStudents
                                                 
                                                  select new StudentDTO
                                                  {
                                                      StudentId = a.IntStudentId,
                                                      StudentName = a.StrStudentName,
                                                      Address = a.StrAddress,
                                                      BloodGroup = a.StrBloodGroup,
                                                      PhoneNo = a.StrPhoneNo
                                                  }).ToList());

                return data;
            
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }
    }
}