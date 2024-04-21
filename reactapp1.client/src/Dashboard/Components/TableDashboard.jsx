import Table from 'react-bootstrap/Table';
import Actions from './Actions.jsx'

function TableDashboard() {
    return (
        <div>

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
                    <tr>
                        <td>1</td>
                        <td>Filan</td>
                        <td>Fisteku</td>
                        <td>true</td>
                        <td>20</td>
                        <td>
                            <Actions/>
                        </td>
                    </tr>
                    <tr>
                        <td>2</td>
                        <td>Filane</td>
                        <td>Gashi</td>
                        <td>false</td>
                        <td>23</td>
                        <td>
                            <Actions/>
                        </td>
                    </tr>
                    <tr>
                        <td>3</td>
                        <td>Arta</td>
                        <td>Gashi</td>
                        <td>true</td>
                        <td>30</td>
                        <td>
                            <Actions />
                        </td>
                    </tr>
                </tbody>
            </Table>
        </div>
    );
}

export default TableDashboard;