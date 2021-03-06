using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class Board
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private string name;
        public string Name
        {
            get => name;
            private set => name = value;
        }

        private string creator;
        public string Creator
        {
            get => creator;
            private set => creator = value;
        }

        private readonly List<Column> columns; // backlogs , inProgress, done (generic updatable)
        public List<Column> Columns
        {
            get => columns;
        }

        private BoardDTO dto;
        public BoardDTO DTO
        {
            get => dto;
            private set => dto = value;
        }

        public Board(string name, String creator)
        {
            Name = name;
            Creator = creator;
            columns = new List<Column> { new Column("backlog", creator, Name, 0), new Column("in progress", creator, Name, 1), new Column("done", creator, Name, 2) };
            dto = new BoardDTO(creator, name, new List<string>(), columns.Select(col => col.DTO).ToList());
            dto.Insert();
            dto.InsertNewBoardMember(creator);
        }

        public Board(BoardDTO boardDTO)
        {
            Name = boardDTO.Boardname;
            Creator = boardDTO.Creator;
            columns = boardDTO.Columns.Select((col) => new Column(col)).ToList();
            dto = boardDTO;
        }

        // pre condition: columnOrdinal < DONE_COLUMN
        public MFResponse AdvanceTask(Task task, int columnOrdinal)
        {
            int taskId = task.ID;

            Columns[columnOrdinal + 1].AddTask(task);
            Columns[columnOrdinal].RemoveTask(task);
            task.DTO.ColumnOrdinal++;
            //try
            //{
            //    Columns[columnOrdinal + 1].AddTask(task);
            //    Columns[columnOrdinal].RemoveTask(task);
            //    task.DTO.ColumnOrdinal++;
            //}
            //catch(Exception e)
            //{
            //    return new MFResponse(e.Message);
            //}
            return new MFResponse();
        }

        public MFResponse LimitColumn(int columnOrdinal, int limit)
        {
            if (columnOrdinal >= columns.Count || columnOrdinal < 0)
                return new MFResponse("there is no such column number");
            Columns[columnOrdinal].MaxTasks = limit;
            return new MFResponse();
    }

    public MFResponse<int> GetColumnLimit(int columnOrdinal)
        {
            if (columnOrdinal >= columns.Count || columnOrdinal < 0)
                return MFResponse<int>.FromError("there is no such column number");
            return MFResponse<int>.FromValue(Columns[columnOrdinal].MaxTasks);
        }

        public MFResponse<string> GetColumnName(int columnOrdinal)
        {
            if (columnOrdinal >= columns.Count || columnOrdinal < 0)
                return MFResponse<string>.FromError("there is no such column number");
            return MFResponse<string>.FromValue(columns[columnOrdinal].Name);
        }

        internal MFResponse<Task> AddTask(DateTime dueDate, string title, string description, string userEmail)
        {
            Task task;

            task = Columns[0].AddTask(dueDate, title, description, userEmail);
            //try
            //{
            //    task = Columns[0].AddTask(dueDate, title, description, userEmail);
            //}
            //catch(Exception a)
            //{
            //    return MFResponse<Task>.FromError(a.Message);
            //}
            return MFResponse<Task>.FromValue(task);
        }

        internal MFResponse<IList<Task>> GetColumn(int columnOrdinal)
        {
            return MFResponse<IList<Task>>.FromValue(Columns[columnOrdinal].Tasks);
        }

        internal MFResponse<IList<Column>> getColumns()
        {
            return MFResponse<IList<Column>>.FromValue(Columns);
        }

        internal MFResponse AddColumn(int columnOrdinal, string columnName)
        {
            if (columnOrdinal > Columns.Count || columnOrdinal < 0)
                return new MFResponse($"Can not insert to index {columnOrdinal}, max {Columns.Count}");
            for (int i = Columns.Count - 1; i >= columnOrdinal; i--)
                Columns[i].ColumnOrdinal++;
            Columns.Insert(columnOrdinal, new Column(columnName, Creator, Name, columnOrdinal));
            return new MFResponse();
        }

        internal MFResponse RemoveColumn(int columnOrdinal)
        {
            if (columnOrdinal >= Columns.Count || columnOrdinal < 0)
                return new MFResponse($"Can not remove column in index {columnOrdinal}, max {Columns.Count}");
            if (Columns.Count <= 2)
                return new MFResponse($"Can not remove any columns, the minimum that is possible is {Columns.Count}");
            Column srcCol = Columns[columnOrdinal];

            Column destCol;
            IList<Task> tasks = srcCol.Tasks;

            if (columnOrdinal == 0)
                destCol = Columns[1];
            else
                destCol = Columns[columnOrdinal - 1];

            destCol.AddTasks(tasks); // throws an exeption if tasks exceeded the max limit
            Columns.RemoveAt(columnOrdinal);
            srcCol.Remove();
            for (int i = columnOrdinal; i < Columns.Count; i++)
                Columns[i].ColumnOrdinal = i;
            //try
            //{
            //    Column destCol;
            //    IList<Task> tasks = srcCol.Tasks;

            //    if (columnOrdinal == 0)
            //        destCol = Columns[1];
            //    else
            //        destCol = Columns[columnOrdinal - 1];

            //    destCol.AddTasks(tasks); // throws an exeption if tasks exceeded the max limit
            //    Columns.RemoveAt(columnOrdinal);
            //    srcCol.Remove();
            //    for (int i = columnOrdinal; i < Columns.Count; i++)
            //        Columns[i].ColumnOrdinal = i;
            //}
            //catch (Exception e)
            //{
            //    return new MFResponse(e.Message);
            //}
            return new MFResponse();
        }

        //internal MFResponse RemoveColumn(int columnOrdinal)
        //{
        //    if (columnOrdinal >= Columns.Count || columnOrdinal < 0)
        //        return new MFResponse($"Can not remove column in index {columnOrdinal}, max {Columns.Count}");
        //    if (Columns.Count < 2)
        //        return new MFResponse($"Can not remove any columns, the minimum that is possible is {Columns.Count}");
        //    Column srcCol = Columns[columnOrdinal];
        //    IList<Task> tasks = srcCol.Tasks;
        //    if (columnOrdinal == 0)
        //    {
        //        Column destCol = Columns[1];
        //        if (destCol.MaxTasks < (destCol.Tasks.Count + srcCol.Tasks.Count))
        //            return new MFResponse("tasks exceeded the limit");
        //        Columns.RemoveAt(0);
        //        foreach (Task task in tasks)
        //            Columns[1].AddTask(task);
        //        for (int i = 0; i < Columns.Count; i++)
        //            Columns[i].ColumnOrdinal = i;
        //    }
        //    else
        //    {
        //        Column destCol = Columns[columnOrdinal - 1];
        //        if (destCol.MaxTasks < (destCol.Tasks.Count + srcCol.Tasks.Count))
        //            return new MFResponse("tasks exceeded the limit");
        //        Columns.RemoveAt(columnOrdinal);
        //        foreach (Task task in tasks)
        //            Columns[columnOrdinal - 1].AddTask(task);
        //        for (int i = columnOrdinal; i < Columns.Count; i++)
        //            Columns[i].ColumnOrdinal = i;
        //    }
        //    return new MFResponse();
        //}

        internal MFResponse RenameColumn(int columnOrdinal, string newColumnName)
        {
            if (columnOrdinal >= Columns.Count || columnOrdinal < 0)
                return new MFResponse($"Can not rename column in index {columnOrdinal}, max {Columns.Count}");
            Columns[columnOrdinal].Name = newColumnName;
            return new MFResponse();
        }

        internal MFResponse MoveColumn(int columnOrdinal, int shiftSize)
        {
            if (columnOrdinal >= Columns.Count || columnOrdinal < 0)
                return new MFResponse($"Can not move column column in index {columnOrdinal}, max {Columns.Count}");
            int loc = columnOrdinal + shiftSize;
            if (loc >= Columns.Count || loc < 0)
                return new MFResponse($"Can not move column to index {columnOrdinal + shiftSize}");
            Column col = Columns[columnOrdinal];
            if (col.Tasks.Count > 0)
                return new MFResponse($"Only empty columns can be moved");
            col.ColumnOrdinal = Columns.Count; // so there will be no keys conflicts in the DB
            Columns.RemoveAt(columnOrdinal);
            Columns.Insert(loc, col);
            if(shiftSize < 0)
                for (int i = Columns.Count - 1; i >= 0 ; i--)
                    Columns[i].ColumnOrdinal = i;
            else
                for(int i = 0; i < Columns.Count; i++)
                    Columns[i].ColumnOrdinal = i;
            return new MFResponse();
        }

    }
}

