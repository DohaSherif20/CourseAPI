import { useEffect, useState } from "react";
import api from "../services/api";

export default function Courses() {
  const [courses, setCourses] = useState([]);

  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");

  const [editingId, setEditingId] = useState(null);
  const [message, setMessage] = useState("");

const [role, setRole] = useState("");

useEffect(() => {
  setRole(localStorage.getItem("role"));
}, []);

  useEffect(() => {
    fetchCourses();
  }, []);

  const fetchCourses = async () => {
    try {
      const res = await api.get("/Courses");
      setCourses(res.data);
    } catch (error) {
      console.error(error);
    }
  };

  const saveCourse = async () => {
    try {
      if (editingId) {
        await api.put(`/Courses/${editingId}`, {
          title,
          description,
          instructorId: 2
        });
      } else {
        await api.post("/Courses", {
          title,
          description,
          instructorId: 2
        });
      }

      fetchCourses();
      setTitle("");
      setDescription("");
      setEditingId(null);
      setMessage("Saved successfully");
    } catch (error) {
      setMessage("Error saving course");
      console.error(error.response?.data);
    }
  };

  const deleteCourse = async (id) => {
    try {
      await api.delete(`/Courses/${id}`);
      fetchCourses();
    } catch (error) {
      console.error(error.response?.data);
    }
  };

  const startEdit = (course) => {
    setTitle(course.title);
    setDescription(course.description);
    setEditingId(course.id);
  };

  return (
    <div>
      <h1>Courses</h1>

      {/* 🔥 Admin only */}
      {role === "Admin" && (
        <div>
          <h3>{editingId ? "Update Course" : "Add Course"}</h3>

          <input
            type="text"
            placeholder="Title"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
          />

          <input
            type="text"
            placeholder="Description"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
          />

          <button onClick={saveCourse}>
            {editingId ? "Update" : "Add"}
          </button>
        </div>
      )}

      {message && <p>{message}</p>}

      <ul>
        {courses.map((course) => (
          <li key={course.id}>
            {course.title} - {course.description}

            {role === "Admin" && (
              <>
                <button onClick={() => deleteCourse(course.id)}>
                  Delete
                </button>

                <button onClick={() => startEdit(course)}>
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