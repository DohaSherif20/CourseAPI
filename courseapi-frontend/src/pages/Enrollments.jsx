import { useState } from "react";
import api from "../services/api";

export default function Enrollments() {
  const [studentId, setStudentId] = useState("");
  const [courseId, setCourseId] = useState("");

  const enroll = async () => {
    try {
      await api.post("/Enrollments", {
        studentId: Number(studentId),
        courseId: Number(courseId)
      });

      alert("Enrolled successfully");
    } catch (error) {
      console.error(error.response?.data);
      alert("Error enrolling");
    }
  };

  return (
    <div>
      <h1>Enroll Student</h1>

      <input
        type="number"
        placeholder="Student ID"
        value={studentId}
        onChange={(e) => setStudentId(e.target.value)}
      />

      <input
        type="number"
        placeholder="Course ID"
        value={courseId}
        onChange={(e) => setCourseId(e.target.value)}
      />

      <button onClick={enroll}>Enroll</button>
    </div>
  );
}