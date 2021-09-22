using System;
using System.Collections.Generic;
 using GSmartHR.Core.Domain.Users;
using GSmartHR.IRepository.Users;
using GSmartHR.Core;

namespace GSmartHR.Services.Users
{
    public class AttendanceService : IAttendanceService
    {
        #region Fields

        private readonly IAttendanceRepository _attendanceRepository;


        #endregion

        #region Ctor

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            this._attendanceRepository = attendanceRepository;
        }

        #endregion

        public void InsertAttendance(Attendance attendance)
        {
            if (attendance == null)
                throw new ArgumentNullException("attendance");

            if (attendance.Id == Guid.Empty)
            {
                attendance.Id = Guid.NewGuid();
            }

            _attendanceRepository.Insert(attendance);
        }
        public void UpdateAttendance(Attendance attendance)
        {

            if (attendance == null)
                throw new ArgumentNullException("attendance");


            _attendanceRepository.Update(attendance);
        }
        public void DeleteAttendance(Attendance attendance)
        {
            if (attendance == null)
                throw new ArgumentNullException("attendance");

   
            _attendanceRepository.Delete(attendance);
        }

        public Attendance GetAttendanceById(Guid attendanceId)
        {
            if (attendanceId == Guid.Empty)
                return null;

            return _attendanceRepository.GetById(attendanceId);
        }

        public IEnumerable<Attendance> GetAllAttendance()
        {

            return _attendanceRepository.GetAll();
        }

        public IEnumerable<AttendanceDetailsContainer> GetAllAttendancesBySearch(string employeeidno, int pageno, int pagesize)
        {
            return _attendanceRepository.GetAllAttendancesBySearch(employeeidno, pageno,pagesize);
        }

        public Attendance GetAttendanceByEmployeeIdAndDate(Guid? employeeId, DateTime date)
        {
            return _attendanceRepository.GetAttendanceByEmployeeIdAndDate(employeeId,date);
        }
    }
}
