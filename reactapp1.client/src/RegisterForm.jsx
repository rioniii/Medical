import React, { useState } from 'react';
import { MDBBtn, MDBContainer, MDBCard, MDBCardBody, MDBInput } from 'mdb-react-ui-kit';
import { NavLink } from 'react-router-dom';

function RegisterForm() {
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [repeatPassword, setRepeatPassword] = useState('');
    const [contactNumber, setContactNumber] = useState('');
    const [passwordMatchError, setPasswordMatchError] = useState('');

    const handleSubmit = async (event) => {
        event.preventDefault();
        if (password !== repeatPassword) {
            setPasswordMatchError("Passwords do not match!");
            return;
        }

        const userData = {
            id: 0,
            name: name,
            email: email,
            password: password,
            confirmPassword: repeatPassword,
            numriKontaktues: contactNumber
        };

        try {
            const response = await fetch('https://localhost:7107/api/User', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(userData),
            });

            if (!response.ok) {
                const errorData = await response.json();
                console.error('Registration failed:', errorData);
                setPasswordMatchError(errorData.title);
                return;
            }

            const data = await response.json();
            console.log(data);
            // Redirect or handle login status here
        } catch (error) {
            console.error('Registration failed:', error);
            setPasswordMatchError('Registration failed. Please try again.');
        }
    };

    return (
        <MDBContainer fluid className='d-flex align-items-center justify-content-center bg-image'>
            <MDBCard className='m-5' style={{ maxWidth: '600px' }}>
                <MDBCardBody className='px-5'>
                    <h2 className="text-uppercase text-center mb-5">Create an account</h2>
                    <form onSubmit={handleSubmit}>
                        <MDBInput wrapperClass='mb-4' label='Your Name' size='lg' id='form1' type='text' value={name} onChange={(e) => setName(e.target.value)} />
                        <MDBInput wrapperClass='mb-4' label='Your Email' size='lg' id='form2' type='email' value={email} onChange={(e) => setEmail(e.target.value)} />
                        <MDBInput wrapperClass='mb-4' label='Password' size='lg' id='form3' type='password' value={password} onChange={(e) => setPassword(e.target.value)} />
                        <MDBInput wrapperClass='mb-4' label='Repeat your password' size='lg' id='form4' type='password' value={repeatPassword} onChange={(e) => setRepeatPassword(e.target.value)} />
                        <MDBInput wrapperClass='mb-4' label='Contact Number' size='lg' id='form5' type='text' value={contactNumber} onChange={(e) => setContactNumber(e.target.value)} />
                        {passwordMatchError && <div className="text-danger">{passwordMatchError}</div>}
                        <MDBBtn className='mb-4 w-100' size='lg' type='submit'>Register</MDBBtn>
                    </form>
                    <NavLink to="/LoginForm"><button className="btn btn-dark">Log in here</button></NavLink>
                </MDBCardBody>
            </MDBCard>
        </MDBContainer>
    );
}

export default RegisterForm;
