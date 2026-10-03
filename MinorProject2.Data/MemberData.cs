using MySql.Data.MySqlClient;
using MinorProject2.Data.Models;

namespace MinorProject2.Data;

public class MemberData
{
    private readonly DatabaseConnection database;

    public MemberData(DatabaseConnection database)
    {
        this.database = database;
    }

    // READ - Get all members
    public List<Member> GetAllMembers()
    {
        List<Member> members = new List<Member>();

        using MySqlConnection connection = database.GetConnection();

        connection.Open();

        string query = """
            SELECT MemberID, FirstName, LastName, Email, Phone
            FROM Members
            """;

        using MySqlCommand command = new MySqlCommand(query, connection);

        using MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Member member = new Member
            {
                MemberID = reader.GetInt32("MemberID"),
                FirstName = reader.GetString("FirstName"),
                LastName = reader.GetString("LastName"),
                Email = reader.GetString("Email"),
                Phone = reader.GetString("Phone")
            };

            members.Add(member);
        }

        return members;
    }

    // CREATE - Add a new member
    public void AddMember(Member member)
    {
        using MySqlConnection connection = database.GetConnection();

        connection.Open();

        string query = """
            INSERT INTO Members (FirstName, LastName, Email, Phone)
            VALUES (@FirstName, @LastName, @Email, @Phone)
            """;

        using MySqlCommand command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@FirstName", member.FirstName);
        command.Parameters.AddWithValue("@LastName", member.LastName);
        command.Parameters.AddWithValue("@Email", member.Email);
        command.Parameters.AddWithValue("@Phone", member.Phone);

        command.ExecuteNonQuery();
    }

    // SEARCH - Search by First Name, Last Name, and Email
    public List<Member> SearchMembers(
        string firstName,
        string lastName,
        string email)
    {
        List<Member> members = new List<Member>();

        using MySqlConnection connection = database.GetConnection();

        connection.Open();

        string query = """
            SELECT MemberID, FirstName, LastName, Email, Phone
            FROM Members
            WHERE 1 = 1
            """;

        if (!string.IsNullOrWhiteSpace(firstName))
        {
            query += " AND FirstName LIKE @FirstName";
        }

        if (!string.IsNullOrWhiteSpace(lastName))
        {
            query += " AND LastName LIKE @LastName";
        }

        if (!string.IsNullOrWhiteSpace(email))
        {
            query += " AND Email LIKE @Email";
        }

        using MySqlCommand command = new MySqlCommand(query, connection);

        if (!string.IsNullOrWhiteSpace(firstName))
        {
            command.Parameters.AddWithValue(
                "@FirstName",
                "%" + firstName + "%");
        }

        if (!string.IsNullOrWhiteSpace(lastName))
        {
            command.Parameters.AddWithValue(
                "@LastName",
                "%" + lastName + "%");
        }

        if (!string.IsNullOrWhiteSpace(email))
        {
            command.Parameters.AddWithValue(
                "@Email",
                "%" + email + "%");
        }

        using MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Member member = new Member
            {
                MemberID = reader.GetInt32("MemberID"),
                FirstName = reader.GetString("FirstName"),
                LastName = reader.GetString("LastName"),
                Email = reader.GetString("Email"),
                Phone = reader.GetString("Phone")
            };

            members.Add(member);
        }

        return members;
    }

    // UPDATE - Update an existing member
    public void UpdateMember(Member member)
    {
        using MySqlConnection connection = database.GetConnection();

        connection.Open();

        string query = """
            UPDATE Members
            SET FirstName = @FirstName,
                LastName = @LastName,
                Email = @Email,
                Phone = @Phone
            WHERE MemberID = @MemberID
            """;

        using MySqlCommand command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@MemberID", member.MemberID);
        command.Parameters.AddWithValue("@FirstName", member.FirstName);
        command.Parameters.AddWithValue("@LastName", member.LastName);
        command.Parameters.AddWithValue("@Email", member.Email);
        command.Parameters.AddWithValue("@Phone", member.Phone);

        command.ExecuteNonQuery();
    }

    // DELETE - Delete a member
    public void DeleteMember(int memberID)
    {
        using MySqlConnection connection = database.GetConnection();

        connection.Open();

        string query = """
            DELETE FROM Members
            WHERE MemberID = @MemberID
            """;

        using MySqlCommand command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@MemberID", memberID);

        command.ExecuteNonQuery();
    }
}