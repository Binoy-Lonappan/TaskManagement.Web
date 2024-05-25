using TaskManagement.Web.Data.Models;

namespace TaskManagement.Web.Data.Providers
{
	public interface ITaskToDoProvider
	{
		List<TasksToDo> GetTasksToDo();
		TasksToDo GetTasksToDoById(int taskid);
		bool SaveTaskToDo(TasksToDo t, out string msg);
		bool UpdateTaskToDo(TasksToDo t, out string msg);
		bool DeleteTaskToDo(int taskid, out string msg);

	}

	public	class TaskToDoProvider: ITaskToDoProvider
	{
		private readonly ApplicationDbContext _context;

		public TaskToDoProvider(ApplicationDbContext context)
		{
			_context = context;
		}

		public List<TasksToDo> GetTasksToDo()
		{
			var r = _context.TasksToDo.ToList();
			foreach(var t in r)
			{                
                if (t.DueDate.Date < DateTime.Now.Date)
                    t.Status = "COMPLETED";
                else if (t.DueDate.Date.Equals(DateTime.Now.Date))
                    t.Status = "INPROGRESS";
				else
					t.Status = "PENDING";
            }
			_context.TasksToDo.UpdateRange(r);
			_context.SaveChanges();
			return r;
		}

		public TasksToDo GetTasksToDoById(int taskid)
		{
			var r = _context.TasksToDo.FirstOrDefault(x=> x.Id.Equals(taskid));
			return r;
		}
		public bool SaveTaskToDo(TasksToDo t, out string msg)
		{
			bool r = false;
			msg = "Unable to save task. Please try again.";
			if(t != null)
			{
				var s = "PENDING";
				if (t.DueDate.Date < DateTime.Now.Date)
					s = "COMPLETED";
				else if (t.DueDate.Date.Equals(DateTime.Now.Date))
					s = "INPROGRESS";				
				t.Status = s;
				t.CreateDateTime = DateTime.Now;
				_context.TasksToDo.Add(t);
				_context.SaveChanges();
				msg = $"Task saved successfully with id: {t.Id}";
				r = true;
			}
			return r;
		}
		public bool UpdateTaskToDo(TasksToDo t, out string msg)
		{
			bool r = false;
			msg = "Unable to update task. Please try again.";

			if(t != null)
			{
				var et = _context.TasksToDo.Where(x => x.Id.Equals(t.Id)).FirstOrDefault();
				if (et != null)
				{
					et.Description = t.Description;
					et.DueDate = t.DueDate;
					et.Status = t.Status;
					et.Title = t.Title;
					_context.SaveChanges();
					msg = $"Task {t.Title} updated successfully.";
					r = true;
				}
				else
					msg = "Task not found for updating.";

			}

			return r;
		}
		public bool DeleteTaskToDo(int taskid,out string msg)
		{
			bool r = false;
			msg = "Unable to delete task. Please try again.";
			if(taskid != 0) {
				var et = _context.TasksToDo.Where(x => x.Id.Equals(taskid)).FirstOrDefault();
				if (et != null)
				{ 
					_context.Remove(et);
					_context.SaveChanges();
					msg = $"Task {et.Title} deleted successfully.";
					r = true;
				}
			}

			return r;
		}
	}

}
