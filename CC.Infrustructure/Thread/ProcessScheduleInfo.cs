
//---------------------------------------------------------------------------
//Name:	ProcessScheduleInfo data entity
//Function: data entity
//Author:	CodeSmith
//Date:    2011-7-7
//---------------------------------------------------------------------------
//Change History:
// Date				Who			Changes Made           Purpose         Comments
//---------------------------------------------------------------------------
//2011-7-7	CodeSmith	Initial creation
//---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Infrustructure.Thread
{

    /// <summary>
    /// ProcessScheduleInfo
    /// </summary>
    public partial class ProcessScheduleInfo
    {
        protected string _system_name = String.Empty;
        protected DateTime _last_run_begin_time;
        protected DateTime _last_run_end_time;
        protected int _last_run_status;
        protected int _run_interval;
        protected int _check_interval;
        protected string _system_parameter1 = String.Empty;
        protected string _system_parameter2 = String.Empty;
        protected string _system_parameter3 = String.Empty;
        protected string _system_parameter4 = String.Empty;
        protected string _system_parameter5 = String.Empty;
        protected Guid _service_fid;

        public ProcessScheduleInfo(string system_name,
DateTime last_run_begin_time,
DateTime last_run_end_time,
int last_run_status,
int run_interval,
int check_interval,
string system_parameter1,
string system_parameter2,
string system_parameter3,
string system_parameter4,
string system_parameter5,
Guid service_fid
        )
        {
            _system_name = system_name;
            _last_run_begin_time = last_run_begin_time;
            _last_run_end_time = last_run_end_time;
            _last_run_status = last_run_status;
            _run_interval = run_interval;
            _check_interval = check_interval;
            _system_parameter1 = system_parameter1;
            _system_parameter2 = system_parameter2;
            _system_parameter3 = system_parameter3;
            _system_parameter4 = system_parameter4;
            _system_parameter5 = system_parameter5;
            _service_fid = service_fid;
        }

        public ProcessScheduleInfo()
        {
        }


        public string System_name
        {
            get { return _system_name; }
            set { _system_name = value; }
        }

        public DateTime Last_run_begin_time
        {
            get { return _last_run_begin_time; }
            set { _last_run_begin_time = value; }
        }

        public DateTime Last_run_end_time
        {
            get { return _last_run_end_time; }
            set { _last_run_end_time = value; }
        }

        public int Last_run_status
        {
            get { return _last_run_status; }
            set { _last_run_status = value; }
        }

        public int Run_interval
        {
            get { return _run_interval; }
            set { _run_interval = value; }
        }

        public int Check_interval
        {
            get { return _check_interval; }
            set { _check_interval = value; }
        }

        public string System_parameter1
        {
            get { return _system_parameter1; }
            set { _system_parameter1 = value; }
        }

        public string System_parameter2
        {
            get { return _system_parameter2; }
            set { _system_parameter2 = value; }
        }

        public string System_parameter3
        {
            get { return _system_parameter3; }
            set { _system_parameter3 = value; }
        }

        public string System_parameter4
        {
            get { return _system_parameter4; }
            set { _system_parameter4 = value; }
        }

        public string System_parameter5
        {
            get { return _system_parameter5; }
            set { _system_parameter5 = value; }
        }

        public Guid Service_fid
        {
            get { return _service_fid; }
            set { _service_fid = value; }
        }


    }

}
