using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/*
CREATE TABLE [dbo].[Teacher] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (100) NULL,
    [LastName]  NVARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Subject] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (50) NOT NULL,
    [Hours]     INT           NOT NULL,
    [TeacherId] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Subject_Teacher] FOREIGN KEY ([TeacherId]) REFERENCES [dbo].[Teacher] ([Id])
);


*/
namespace DB_UserControl
{
    public partial class Form1 : Form
    {
        DBDataContext dbDataContext = new DBDataContext();
        public Form1()
        {
            InitializeComponent();

            userControlSubjectInsert.Teachers = dbDataContext.Teachers.ToArray();
            
            userControlSubjectUpdate.Teachers = dbDataContext.Teachers.ToArray();
            
            getData();
        }

        private void getData()
        {
            listBoxSubjects.Items.Clear();

            listBoxSubjects.Items.AddRange(dbDataContext.Subjects.ToArray());
            userControlSubjectUpdate.MySubject = null;
        }

        private void listBoxSubjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBoxSubjects.SelectedItem is Subject)
            {
                Subject subject = (Subject)listBoxSubjects.SelectedItem;
                userControlSubjectUpdate.MySubject = subject;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxSubjects.SelectedItem is Subject)
            {
                Subject subject = (Subject)listBoxSubjects.SelectedItem;

                dbDataContext.Subjects.DeleteOnSubmit(subject);

                dbDataContext.SubmitChanges();
                getData();
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            dbDataContext.Subjects.InsertOnSubmit(userControlSubjectInsert.MySubject);
            userControlSubjectInsert.MySubject = null;
            dbDataContext.SubmitChanges();
            getData();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (listBoxSubjects.SelectedItem == userControlSubjectUpdate.MySubject)
            {
                dbDataContext.SubmitChanges();
                getData();
            }
        }
    }
}
