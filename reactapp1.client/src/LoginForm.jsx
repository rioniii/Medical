import React, { useState } from 'react';
import { MDBBtn, MDBCard, MDBCardBody, MDBInput } from 'mdb-react-ui-kit';
import { NavLink } from 'react-router-dom';
import contactImage from './assets/R.jpeg'; // Ensure the image path is correct
import Header from './Header'; // Import the Header component

function Register() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState('');

    const handleEmailChange = (event) => setEmail(event.target.value);
    const handlePasswordChange = (event) => setPassword(event.target.value);

    const handleRegister = async (event) => {
        event.preventDefault();


        const userData = {
            email: email,
            password: password,
        };

        try {
            const response = await fetch('https://localhost:7107/api/Auth/Login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(userData),
            });

            if (!response.ok) {
                const errorData = await response.json();
                console.error('Registration failed:', errorData);
                if (errorData.errors && errorData.errors.Email) {
                    setEmailTakenError('Email is already taken!');
                } else {
                    setErrorMessage('Registration failed. Please try again.');
                }
                return;
            }
            const data = await response.json();
            console.log(data);
/*
            if (role == 'user') {
                console.log(data);
            }
            if (role == 'admin') {
                //Linku per dashboard
            }
            if (role == 'mjek') {
                //linku per dashboard te mjekut
            }*/

        } catch (error) {
            console.error('Registration failed:', error);
            setErrorMessage('Registration failed. Please try again.');
        }
    };

    return (
        <div className="register-form-container" style={{
            backgroundImage: `url(${contactImage})`,
            backgroundColor: 'rgba(255, 255, 255, 0.5)',
            backgroundSize: 'cover',
            backgroundPosition: 'center',
            minHeight: '100vh',
            display: 'flex',
            flexDirection: 'column'
        }}>
            <Header />
            <div style={{
                flex: 1,
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                padding: '20px'
            }}>
                <MDBCard className='p-4' style={{
                    maxWidth: '500px',
                    width: '100%',
                    backgroundColor: 'rgba(255, 255, 255, 0.9)',
                    borderRadius: '15px',
                    boxShadow: '0px 0px 10px rgba(0, 0, 0, 0.1)'
                }}>
                    <MDBCardBody className='px-5 py-4'>
                        <h2 className="text-uppercase text-center mb-3">Log In</h2>
                        <form onSubmit={handleRegister}>
                            <MDBInput label='Your Email' size='lg' id='form2' type='email' value={email} onChange={handleEmailChange} />
                            <MDBInput label='Password' size='lg' id='form3' type='password' value={password} onChange={handlePasswordChange} />

                            {errorMessage && <div className="text-danger mb-2">{errorMessage}</div>}
                            <MDBBtn className='w-100' size='lg' type='submit'>LogIn</MDBBtn>
                        </form>
                        <NavLink to="/LoginForm" className="text-center d-block mt-3" style={{ color: 'green' }}>You don't have an account? Register here</NavLink>
                    </MDBCardBody>
                </MDBCard>
            </div>
        </div>
    );
}

export default Register;