import React, { useState } from "react";
import Sidebar from "./Sidebar"; // Import Sidebar component
import "./AnnouncementCard.css"; // Import external CSS file

// AddAnnouncementForm Component
const Announcements = ({ onAddAnnouncement }) => {
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');
    const [startDate, setStartDate] = useState('');
    const [endDate, setEndDate] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();

        // Check if the start date is later than the end date
        if (new Date(startDate) > new Date(endDate)) {
            alert('Start date cannot be later than end date');
            return;
        }

        const newAnnouncement = {
            id: Date.now(),
            title,
            description,
            startDate,
            endDate
        };

        // Pass the new announcement to the parent component
        onAddAnnouncement(newAnnouncement);

        // Reset form fields
        setTitle('');
        setDescription('');
        setStartDate('');
        setEndDate('');
    };

    return (
        <div className="d-flex">
            <Sidebar userType="admin" />  {/* Sidebar for Admin */}

            <div className="add-announcement-form-container" style={{ flex: 1, padding: '20px' }}>
                <form onSubmit={handleSubmit} className="form">
                    <h3>Add New Announcement</h3>
                    <div className="form-group">
                        <label>Title:</label>
                        <input
                            type="text"
                            value={title}
                            onChange={(e) => setTitle(e.target.value)}
                            required
                            className="input"
                        />
                    </div>
                    <div className="form-group">
                        <label>Description:</label>
                        <textarea
                            value={description}
                            onChange={(e) => setDescription(e.target.value)}
                            required
                            className="input"
                        />
                    </div>
                    <div className="form-group">
                        <label>Start Date:</label>
                        <input
                            type="datetime-local"
                            value={startDate}
                            onChange={(e) => setStartDate(e.target.value)}
                            required
                            className="input"
                        />
                    </div>
                    <div className="form-group">
                        <label>End Date:</label>
                        <input
                            type="datetime-local"
                            value={endDate}
                            onChange={(e) => setEndDate(e.target.value)}
                            required
                            className="input"
                        />
                    </div>
                    <button type="submit" className="button">Add Announcement</button>
                </form>
            </div>
        </div>
    );
};

export default Announcements