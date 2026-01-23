using System.Text.Json.Serialization;

namespace RadioScheduler.Models.Auth;

public class User {
	public Guid Id { get; set; } = Guid.NewGuid();

	public string? FirstName { get; set; } = string.Empty;
	public string? LastName { get; set; } = string.Empty;
	public string Username { get; set; } = string.Empty;
	[JsonIgnore]
	public string Password { get; set; } = string.Empty;
	public string? Phone { get; set; } = string.Empty;
	public string? Email { get; set; } = string.Empty;
	public string? Address { get; set; } = string.Empty;
	public string? City { get; set; } = string.Empty;
	public string? ZipCode { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public ICollection<UserRole> Roles { get; } = new List<UserRole>();
	public ICollection<ContributorPayment> ContributorPayments { get; } = new List<ContributorPayment>();

	public User() {
	}

	public User(Guid id, string? firstName, string? lastName, string username, string password, string? phone,
		string? email, string? address, string? city, string? zipcode) {
		this.Id = id;
		this.FirstName = firstName;
		this.LastName = lastName;
		this.Username = username;
		this.Password = password;
		this.Phone = phone;
		this.Email = email;
		this.Address = address;
		this.City = city;
		this.ZipCode = zipcode;
		this.CreatedAt = DateTime.UtcNow;
	}

	public User(User user) {
		this.Id = user.Id;
		this.FirstName = user.FirstName;
		this.LastName = user.LastName;
		this.Username = user.Username;
		this.Phone = user.Phone;
		this.Email = user.Email;
		this.Address = user.Address;
		this.City = user.City;
		this.ZipCode = user.ZipCode;
		this.CreatedAt = user.CreatedAt;
		this.Roles = user.Roles;
	}
}
