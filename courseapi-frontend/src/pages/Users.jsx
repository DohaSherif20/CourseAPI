import { useEffect, useState } from "react";
import api from "../services/api";

export default function Users() {
  const [users, setUsers] = useState([]);

  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [roleInput, setRoleInput] = useState("Student");

  const [editingId, setEditingId] = useState(null);

  const [role, setRole] = useState("");

  useEffect(() => {
    setRole(localStorage.getItem("role"));
  }, []);

  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = async () => {
    try {
      const res = await api.get("/Users");
      setUsers(res.data);
    } catch (error) {
      console.error(error.response?.data);
    }
  };

  const saveUser = async () => {
    try {
      if (editingId) {
        await api.put(`/Users/${editingId}`, {
          name,
          email,
          role: roleInput
        });
      } else {
        // ❗ مش بنستخدم Users لإنشاء
        await api.post("/Auth/register", {
          name,
          email,
          password: "123456",
          role: roleInput
        });
      }

      fetchUsers();
      setName("");
      setEmail("");
      setRoleInput("Student");
      setEditingId(null);
    } catch (error) {
      console.error(error.response?.data);
    }
  };

  const deleteUser = async (id) => {
    try {
      await api.delete(`/Users/${id}`);
      fetchUsers();
    } catch (error) {
      console.error(error.response?.data);
    }
  };

  const startEdit = (user) => {
    setName(user.name);
    setEmail(user.email);
    setRoleInput(user.role);
    setEditingId(user.id);
  };

  return (
    <div>
      <h1>Users</h1>

      {/* 🔥 Admin only */}
      {role === "Admin" && (
        <div>
          <h3>{editingId ? "Update User" : "Add User"}</h3>

          <input
            type="text"
            placeholder="Name"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />

          <input
            type="email"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />

          <select
            value={roleInput}
            onChange={(e) => setRoleInput(e.target.value)}
          >
            <option>Student</option>
            <option>Instructor</option>
            <option>Admin</option>
          </select>

          <button onClick={saveUser}>
            {editingId ? "Update" : "Add"}
          </button>
        </div>
      )}

      <ul>
        {users.map((user) => (
          <li key={user.id}>
            ID: {user.id} | {user.name} - {user.email} ({user.role})

            {role === "Admin" && (
              <>
                <button
                  onClick={() => deleteUser(user.id)}
                  style={{ marginLeft: "10px" }}
                >
                  Delete
                </button>

                <button
                  onClick={() => startEdit(user)}
                  style={{ marginLeft: "10px" }}
                >
                  Edit
                </button>
              </>
            )}
          </li>
        ))}
      </ul>
    </div>
  );
}