/*import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Invoice from './Invoice'; // The Invoice component you previously created

const SelectPatient = () => {
    const [patients, setPatients] = useState([]);
    const [selectedId, setSelectedId] = useState(null);

    useEffect(() => {
        // Fetch the list of patients from the API
        const fetchPatients = async () => {
            try {
                const response = await axios.get('/api/patients');
                setPatients(response.data);
            } catch (error) {
                console.error('Error fetching patients:', error);
            }
        };

        fetchPatients();
    }, []);

    const handleSelectPatient = (Id) => {
        setSelectedId(Id);
    };

    return (
        <div className="container mt-5">
            <h2>Select a Patient</h2>
            <div className="list-group">
                {patients.map((patient) => (
                    <button
                        key={patient.id}
                        className="list-group-item list-group-item-action"
                        onClick={() => handleSelectPatient(patient.id)}
                    >
                        {patient.name}
                    </button>
                ))}
            </div>

            {selectedId && (
                <div className="mt-5">
                    <h3>Invoice for Selected Patient</h3>
                    <Invoice Id={selectedId} />
                </div>
            )}
        </div>
    );
};

export default SelectPatient;
*/