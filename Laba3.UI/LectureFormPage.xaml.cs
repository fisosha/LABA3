using Laba3.Models;

namespace Laba3.UI;

public partial class LectureFormPage : ContentPage
{
    public event EventHandler<Lecture> LectureSaved;

    private Lecture _lecture;

    public LectureFormPage(Lecture lecture = null)
    {
        InitializeComponent();
        _lecture = lecture ?? new Lecture();
        BindLectureData();
    }

    private void BindLectureData()
    {
        DayEntry.Text = _lecture.Day;
        TimeEntry.Text = _lecture.Time;
        LecturerEntry.Text = _lecture.Lecturer;
        DepartmentEntry.Text = _lecture.Department;
        RoomEntry.Text = _lecture.Room;
        StudentsListView.ItemsSource = _lecture.Students;
    }

    private void OnAddStudentClicked(object sender, EventArgs e)
    {
        string studentName = StudentNameEntry.Text;
        string studentGroup = StudentGroupEntry.Text;

        if (string.IsNullOrWhiteSpace(studentName) || string.IsNullOrWhiteSpace(studentGroup))
        {
            DisplayAlert("Помилка", "Введіть дані студента.", "ОК");
            return;
        }

        _lecture.Students.Add(new Student { Name = studentName, Group = studentGroup });
        StudentsListView.ItemsSource = null;
        StudentsListView.ItemsSource = _lecture.Students;

        StudentNameEntry.Text = string.Empty;
        StudentGroupEntry.Text = string.Empty;
    }

    private void OnDeleteStudentClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Student student)
        {
            _lecture.Students.Remove(student);
            StudentsListView.ItemsSource = null;
            StudentsListView.ItemsSource = _lecture.Students;
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        _lecture.Day = DayEntry.Text;
        _lecture.Time = TimeEntry.Text;
        _lecture.Lecturer = LecturerEntry.Text;
        _lecture.Department = DepartmentEntry.Text;
        _lecture.Room = RoomEntry.Text;

        LectureSaved?.Invoke(this, _lecture);

        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
