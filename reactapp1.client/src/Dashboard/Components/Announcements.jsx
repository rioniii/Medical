import React, { useState, useEffect } from 'react';
import './AnnouncementCard.css'; // Importing the external CSS file
import Sidebar from './Sidebar';  // Import the Sidebar component

// AnnouncementCard Component
const AnnouncementCard = ({ title, description, startDate, endDate, onDelete, id }) => {
    // Format start and end dates to include both date and time
    const formattedStartDate = new Date(startDate).toLocaleString();
    const formattedEndDate = new Date(endDate).toLocaleString();

    return (
        <div className="card">
            <h3 className="title">{title}</h3>
            <p className="description">{description}</p>
            <p className="dates">
                <strong>Start Date:</strong> {formattedStartDate}<br />
                <strong>End Date:</strong> {formattedEndDate}
            </p>
            <button className="delete-button" onClick={() => onDelete(id)}>Delete</button>
        </div>
    );
};

// AddAnnouncementForm Component
const AddAnnouncementForm = ({ onAddAnnouncement }) => {
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
            id: Date.now(), // Unique id for each announcement
            title,
            description,
            startDate,
            endDate
        };

        onAddAnnouncement(newAnnouncement);

        // Reset form fields
        setTitle('');
        setDescription('');
        setStartDate('');
        setEndDate('');
    };

    return (
        <div className="add-announcement-form-container">
            <Sidebar userType="admin" />  {/* Sidebar for Admin */}

            <form onSubmit={handleSubmit} className="form">
                <h2>Add New Announcement</h2>
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
    );
};

// Main AnnouncementList Component
const AnnouncementList = () => {
    const [announcements, setAnnouncements] = useState([]);

    useEffect(() => {
        // Retrieve announcements from localStorage when the component is mounted
        const savedAnnouncements = JSON.parse(localStorage.getItem('announcements')) || [];
        setAnnouncements(savedAnnouncements);
    }, []);

    const addAnnouncement = (newAnnouncement) => {
        const updatedAnnouncements = [...announcements, newAnnouncement];
        setAnnouncements(updatedAnnouncements);

        // Save updated announcements to localStorage
        localStorage.setItem('announcements', JSON.stringify(updatedAnnouncements));
    };

    const deleteAnnouncement = (id) => {
        const updatedAnnouncements = announcements.filter((announcement) => announcement.id !== id);
        setAnnouncements(updatedAnnouncements);

        // Save updated announcements to localStorage after deleting
        localStorage.setItem('announcements', JSON.stringify(updatedAnnouncements));
    };

    return (
        <div className="main-container">
            <h2>Announcements</h2>
            <AddAnnouncementForm onAddAnnouncement={addAnnouncement} />
            <div className="cards-container">
                {announcements.length > 0 ? (
                    announcements.map((announcement) => (
                        <AnnouncementCard
                            key={announcement.id}
                            id={announcement.id}
                            title={announcement.title}
                            description={announcement.description}
                            startDate={announcement.startDate}
                            endDate={announcement.endDate}
                            onDelete={deleteAnnouncement}
                        />
                    ))
                ) : (
                    <p>No announcements available.</p>
                )}
            </div>
        </div>
    );
};

export default AnnouncementList;
