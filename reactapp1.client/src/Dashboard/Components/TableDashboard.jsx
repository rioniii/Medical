import React, { useState, useEffect } from 'react';
import Table from 'react-bootstrap/Table';
import Actions from './Actions.jsx';
import RepartCrud from './RepartCrud.jsx';
import axios from 'axios';

const TableDashboard = () => {
    const [data, setData] = useState([]);

    useEffect(() => {
        getData();
    }, []);

    const getData = () => {
        axios.get('https://localhost:7107/api/YourEndpoint') // Update the endpoint to fetch your data
            .then(response => {
                setData(response.data);
            })
            .catch(error => {
                console.error('Error fetching data:', error);
            });
    };

    return (
        <div>
            <RepartCrud />
            <Table responsive="md">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Surname</th>
                        <th>isRegistered</th>
                        <th>Age</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {data && data.length > 0 ? (
                        data.map((item, index) => (
                            <tr key={index}>
                                <td>{item.Id}</td>
                                <td>{item.Name}</td>
                                <td>{item.Surname}</td>
                                <td>{item.isRegistered}</td>
                                <td>{item.age}</td>
                                <td><Actions /></td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td colSpan="6" style={{ textAlign: "center" }}>Loading...</td>
                        </tr>
                    )}
                </tbody>
            </Table>
        </div>
    );
};

export default TableDashboard;
