using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.ViewModel
{
    class NewTaskViewModel : NotifiableObject
    {
        private BackendController controller;
        private UserModel user;
        private string _taskTitle;
        public string TaskTitle { get => _taskTitle; set { this._taskTitle = value; RaisePropertyChanged("TaskTitle"); } }
        
        private string _taskDescription;
        public string TaskDescription { get => _taskDescription; set { this._taskDescription = value; RaisePropertyChanged("TaskDescription"); } }

        private string _taskDueDate; 
        public  string TaskDueDate { get => _taskDueDate; set { this._taskDueDate = value; RaisePropertyChanged("TaskDueDate"); } }

        //private string _emailAsignee;
        //public string EmailAsignee { get => _emailAsignee; set { this._emailAsignee = value; RaisePropertyChanged("EmailAsignee"); } }

        public BoardModel Board { get; set; }

        public NewTaskViewModel(UserModel user, BoardModel board)
        {
            this.controller = user.Controller;
            this.user = user;
            this.Board = board;
            
        }

        public void AddTask()
        {
            try
            {
                //need to add task to tasks observerList
                //controller.AddTask(user.Email, Board.Creator, Board.Name, TaskTitle, TaskDescription, DateTime.Parse(TaskDueDate), DateTime.Now, user.Email);
                
                TaskModel t = new TaskModel(controller, TaskTitle, TaskDescription, DateTime.Parse(TaskDueDate), user.Email, DateTime.Now, user.Email, Board.Name, Board.Creator, 0);
                Board.Columns[0].AddTask(t);

                MessageBox.Show("Task Added Successfully!");

            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot add new Task. " + e.Message);

            }
        }

    }
}


//class AddBoardViewModel : NotifiableObject
//{
//    private BackendController controller;
//    private UserModel user;
//    private string _boardName;
//    public string BoardName { get => _boardName; set { this._boardName = value; RaisePropertyChanged("BoardName"); } }

//    public AddBoardViewModel(UserModel user)
//    {
//        this.controller = user.Controller;
//        this.user = user;

//    }

//    public void AddBoard()
//    {
//        try
//        {
//            user.AddBoard(BoardName);
//            MessageBox.Show("Board Added Successfully!");

//        }
//        catch (Exception e)
//        {
//            MessageBox.Show("Cannot add new board. " + e.Message);

//        }
//    }



