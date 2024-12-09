import React, { useState, useEffect } from "react";
import axios from "axios";
import Sidebar from "./Sidebar"; // Import Sidebar component
import './Room.css'; // Import CSS for styling

const RoomPatient = () => {
    // Form state
    const [formData, setFormData] = useState({
        pacientId: "",
        dhomaId: "",
        checkInDate: "",
        checkOutDate: "",
    });

    // Error state
    const [error, setError] = useState("");

    // State for storing fetched data
    const [roomAssignments, setRoomAssignments] = useState([]);
    const [rooms, setRooms] = useState([]);

    // Fetch room assignments and rooms data when the component mounts
    useEffect(() => {
        const fetchRoomData = async () => {
            try {
                const roomAssignmentsResponse = await axios.get("https://localhost:7107/api/DhomaPacientit");
                const roomsResponse = await axios.get("https://localhost:7107/api/Dhoma");

                setRoomAssignments(roomAssignmentsResponse.data); // Set the fetched room assignments
                setRooms(roomsResponse.data); // Set the fetched rooms
            } catch (err) {
                setError("Error fetching data: " + err.message);
                console.error(err);
            }
        };

        fetchRoomData();
    }, []); // Empty dependency array means it will only run once when the component mounts

    // Handle form field changes
    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    // Handle date formatting (convert to YYYY-MM-DD format)
    const formatDate = (dateString) => {
        return new Date(dateString).toISOString().split("T")[0];
    };

    // Handle form submission (POST to API)
    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            // Prepare data for submission
            const newEntry = {
                pacientId: formData.pacientId,
                dhomaId: formData.dhomaId,
                checkInDate: formatDate(formData.checkInDate),
                checkOutDate: formData.checkOutDate ? formatDate(formData.checkOutDate) : null,
            };

            // Send data to the backend API
            const response = await axios.post("https://localhost:7107/api/DhomaPacientit", newEntry);

            // Reset form after successful submission
            setFormData({
                pacientId: "",
                dhomaId: "",
                checkInDate: "",
                checkOutDate: "",
            });
            alert("Patient room assignment added successfully!");

            // Fetch updated room assignments
            fetchRoomData();
        } catch (err) {
            setError("Error adding entry: " + err.message);
            console.error(err);
        }
    };

    return (
        <div style={{ display: 'flex', minHeight: '100vh', backgroundColor: '#f5f6f7' }}>
            {/* Sidebar Component */}
            <Sidebar userType="doctor" />


            <div className="container">
                <h2>Add Patient Room Assignment</h2>

                {/* Display error if any */}
                {error && <div className="error">{error}</div>}

                {/* Form for Room Patient assignment */}
                <form onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label htmlFor="pacientId">Patient ID</label>
                        <input
                            type="text"
                            id="pacientId"
                            name="pacientId"
                            value={formData.pacientId}
                            onChange={handleChange}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="dhomaId">Room</label>
                        <select
                            id="dhomaId"
                            name="dhomaId"
                            value={formData.dhomaId}
                            onChange={handleChange}
                            required
                        >
                            <option value="">Select Room</option>
                            {rooms.map((room) => (
                                <option key={room.id} value={room.id}>
                                    {room.nrDhomes} - {room.llojiDhomes} (Capacity: {room.kapaciteti})
                                </option>
                            ))}
                        </select>
                    </div>

                    <div className="form-group">
                        <label htmlFor="checkInDate">Check-in Date</label>
                        <input
                            type="date"
                            id="checkInDate"
                            name="checkInDate"
                            value={formData.checkInDate}
                            onChange={handleChange}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="checkOutDate">Check-out Date (Optional)</label>
                        <input
                            type="date"
                            id="checkOutDate"
                            name="checkOutDate"
                            value={formData.checkOutDate}
                            onChange={handleChange}
                        />
                    </div>

                    {/* Submit button */}
                    <button type="submit" className="submit-button">
                        Add Room Assignment
                    </button>
                </form>

                {/* Display the list of room assignments */}
                <h3>Existing Room Assignments</h3>
                <table className="table">
                    <thead>
                        <tr>
                            <th>Patient ID</th>
                            <th>Room Details</th>
                            <th>Check-in Date</th>
                            <th>Check-out Date</th>
                        </tr>
                    </thead>
                    <tbody>
                        {roomAssignments.length === 0 ? (
                            <tr>
                                <td colSpan="4">No room assignments available.</td>
                            </tr>
                        ) : (
                            roomAssignments.map((assignment) => {
                                const room = rooms.find((r) => r.id === assignment.dhomaId); // Get room details by ID
                                return (
                                    <tr key={assignment.id}>
                                        <td>{assignment.pacientId}</td>
                                        <td>
                                            {room ? (
                                                <div>
                                                    {room.nrDhomes} - {room.llojiDhomes} (Capacity: {room.kapaciteti})
                                                </div>
                                            ) : (
                                                "Room details not available"
                                            )}
                                        </td>
                                        <td>{new Date(assignment.checkInDate).toLocaleDateString()}</td>
                                        <td>{assignment.checkOutDate ? new Date(assignment.checkOutDate).toLocaleDateString() : "N/A"}</td>
                                    </tr>
                                );
                            })
                        )}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default RoomPatient;
