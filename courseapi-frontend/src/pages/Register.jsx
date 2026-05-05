import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import api from "../services/api";

export default function Register() {
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    name: "",
    email: "",
    password: "",
    role: "Student"
  });

  const [message, setMessage] = useState("");

  const handleChange = (e) => {
    const { name, value } = e.target;

    setFormData({
      ...formData,
      [name]: value
    });
  };

  const handleRegister = async (e) => {
    e.preventDefault();

    try {
      await api.post("/Auth/register", formData);

      setMessage("Registered successfully");
      navigate("/");
    } catch (error) {
      console.log(error.response);

      const errorData = error.response?.data;

      if (errorData?.errors) {
        const errors = Object.values(errorData.errors).flat().join(" ");
        setMessage(errors);
      } else {
        setMessage(errorData?.message || errorData?.title || "Error registering user");
      }
    }
  };

  return (
    <div>
      <h1>Register</h1>

      <form onSubmit={handleRegister}>
        <input
          type="text"
          name="name"
          placeholder="Name"
          value={formData.name}
          onChange={handleChange}
          required
        />

        <input
          type="email"
          name="email"
          placeholder="Email"
          value={formData.email}
          onChange={handleChange}
          required
        />

        <input
          type="password"
          name="password"
          placeholder="Password"
          value={formData.password}
          onChange={handleChange}
          required
        />

        <select name="role" value={formData.role} onChange={handleChange}>
          <option value="Student">Student</option>
          <option value="Instructor">Instructor</option>
        </select>

        <button type="submit">Register</button>
      </form>

      {message && <p>{message}</p>}

      <p>
        Already have an account? <Link to="/">Login</Link>
      </p>
    </div>
  );
}