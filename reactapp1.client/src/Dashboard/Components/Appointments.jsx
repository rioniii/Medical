// src/components/Appointments.js
import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import axios from "axios";

const Appointments = () => {
  const { doctorId } = useParams(); // Get doctorId from the URL
  const [appointments, setAppointments] = useState([]);
  const [error, setError] = useState("");

  // Function to fetch appointments for a specific doctor
  const fetchAppointments = async () => {
    try {
      const response = await axios.get(`https://localhost:7107/api/Termini/doctor/${doctorId}`);
      setAppointments(response.data);
    } catch (err) {
      setError("Failed to fetch appointments.");
    }
  };

  useEffect(() => {
    fetchAppointments();
  }, [doctorId]);

  return (
    <div>
      <h1>Appointments for Doctor {doctorId}</h1>
      {error && <p style={{ color: "red" }}>{error}</p>}
      {appointments.length === 0 ? (
        <p>No appointments found.</p>
      ) : (
        <table>
          <thead>
            <tr>
              <th>Appointment Date</th>
              <th>Patient Name</th>
              <th>Patient Surname</th>
              <th>Check-In</th>
              <th>Check-Out</th>
            </tr>
          </thead>
          <tbody>
            {appointments.map((appointment) => (
              <tr key={appointment.id}>
                <td>{new Date(appointment.appointmentDate).toLocaleString()}</td>
                <td>{appointment.patientName}</td>
                <td>{appointment.patientSurname}</td>
                <td>{appointment.checkInDate ? new Date(appointment.checkInDate).toLocaleString() : "Not Checked In"}</td>
                <td>{appointment.checkOutDate ? new Date(appointment.checkOutDate).toLocaleString() : "Not Checked Out"}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default Appointments;
