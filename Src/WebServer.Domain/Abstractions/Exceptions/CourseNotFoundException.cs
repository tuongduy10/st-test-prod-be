namespace WebServer.Domain.Abstractions.Exceptions;

public class CourseNotFoundException : NotFoundException
{
    public CourseNotFoundException(string courseName) 
        : base($"Course {courseName} is not found") { }
}
