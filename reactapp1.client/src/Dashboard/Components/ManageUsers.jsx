import React, { useEffect, useState } from "react";
import axios from "axios";
import Sidebar from './Sidebar'; // Assuming Sidebar is a separate component
import './ManageUsers.css'; // CSS for styling

const ManageUsers = () => {
    const [users, setUsers] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchUsers();
    }, []);

    const fetchUsers = async () => {
        try {
            setLoading(true);
            const response = await axios.get("https://localhost:7107/api/User"); // Updated URL
            if (Array.isArray(response.data)) {
                setUsers(response.data); // Set users if response is an array
            } else {
                throw new Error("Invalid data format: users should be an array");
            }
            setError(null);
        } catch (err) {
            setError("Failed to fetch users. Please try again.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="d-flex" style={{ backgroundColor: '#f5f6f7' }}>
            {/* Sidebar for admin */}
            <Sidebar userType="admin" />

            <div className="admin-dashboard">
                {error && <div className="error">{error}</div>}
                {loading ? (
                    <div className="loading">Loading...</div>
                ) : (
                    <div className="user-list">
                        <h2>Users</h2>
                        <table className="user-table">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Name</th>
                                    <th>Email</th>
                                    <th>Email Confirmed</th>
                                    <th>Refresh Tokens</th>
                                </tr>
                            </thead>
                            <tbody>
                                {users.length > 0 ? (
                                    users.map((user) => (
                                        <tr key={user.id}>
                                            <td>{user.id}</td>
                                            <td>{`${user.firstName} ${user.lastName}`}</td>
                                            <td>{user.email}</td>
                                            <td>{user.emailConfirmed ? "Yes" : "No"}</td>
                                            <td>
                                                {user.refreshTokens.length > 0 ? (
                                                    <table className="refresh-token-table">
                                                        <thead>
                                                            <tr>
                                                                <th>Token</th>
                                                                <th>Expires</th>
                                                                <th>Is Expired</th>
                                                                <th>Is Active</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            {user.refreshTokens.map((token, index) => (
                                                                <tr key={index}>
                                                                    <td>{token.token}</td>
                                                                    <td>{new Date(token.expires).toLocaleString()}</td>
                                                                    <td>{token.isExpired ? "Yes" : "No"}</td>
                                                                    <td>{token.isActive ? "Yes" : "No"}</td>
                                                                </tr>
                                                            ))}
                                                        </tbody>
                                                    </table>
                                                ) : (
                                                    <p>No refresh tokens available.</p>
                                                )}
                                            </td>
                                        </tr>
                                    ))
                                ) : (
                                    <tr>
                                        <td colSpan="5">No users available.</td>
                                    </tr>
                                )}
                            </tbody>
                        </table>
                    </div>
                )}
            </div>
        </div>
    );
};

export default ManageUsers;
