import React, { useState, useEffect } from 'react';
import { MDBBtn, MDBCard, MDBCardBody, MDBInput } from 'mdb-react-ui-kit';
import { NavLink } from 'react-router-dom';
import contactImage from './assets/R.jpeg'; // Ensure the image path is correct
import axios from 'axios';

function Register() {
    document.title = "Register";

    // Redirect if user is already registered
    useEffect(() => {
        const user = localStorage.getItem("User");
        if (user) {
            document.location = "/";
        }
    }, []);

    const [username, setUsername] = useState('');
    const [fullName, setFullName] = useState('');
    const [lastName, setLastName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [dateOfBirth, setDateOfBirth] = useState('');
    const [errorMessage, setErrorMessage] = useState('');
    const [passwordMatchError, setPasswordMatchError] = useState('');
    const [emailTakenError, setEmailTakenError] = useState('');

    const handleRegister = async (event) => {
        event.preventDefault();

        // Reset errors
        setPasswordMatchError('');
        setEmailTakenError('');
        setErrorMessage('');

        const today = new Date();
        const selectedDate = new Date(dateOfBirth);

        if (selectedDate > today) {
            setErrorMessage('Date of Birth cannot be in the future.');
            return;
        }
        // Check password strength
        const passwordRegex = /^(?=.*[A-Z])(?=.*[!@#$%^&*(),.?":{}|<>])[A-Za-z\d!@#$%^&*(),.?":{}|<>]{6,}$/;
        if (!passwordRegex.test(password)) {
            setPasswordMatchError('Password must be at least 6 characters long, contain one uppercase letter, and one special character.');
            return;
        }

        // Check if passwords match
        if (password !== confirmPassword) {
            setPasswordMatchError('Passwords do not match.');
            return;
        }

        const userData = {
            Username: username,
            FirstName: fullName,
            LastName: lastName,
            Email: email,
            Password: password,
            Ditelindja: dateOfBirth
        };

        try {
            // API call to backend for registration
            const token = localStorage.getItem("token");
            const response = await axios.post(
                'https://localhost:7107/api/User/register',
                userData,
                { headers: { Authorization: `Bearer ${token}` } }
            );

            if (!response.ok) {
                const errorData = await response.json();
                console.error('Registration failed:', errorData);

                // Check specific error messages
                if (errorData.title === 'Email already taken') {
                    setEmailTakenError('Email is already taken!');
                } else {
                    setErrorMessage(errorData.title || 'Registration failed. Please try again.');
                }
                return;
            }

            console.log('Registration successful');
            if (response.data.token && response.data.token.split('.').length === 3) {
                localStorage.setItem("token", response.data.token);
            } else {
                console.error('Invalid token received');
            }
            document.location = "/login"; // Redirect to login page

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
            flexDirection: 'column',
        }}>
            <div style={{
                flex: 1,
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                padding: '10px',
            }}>
                <MDBCard className='p-3' style={{
                    maxWidth: '400px',
                    width: '100%',
                    backgroundColor: 'rgba(255, 255, 255, 0.9)',
                    borderRadius: '10px',
                    boxShadow: '0px 0px 5px rgba(0, 0, 0, 0.1)',
                }}>
                    <MDBCardBody className='px-4 py-3'>
                        <h2 className="text-uppercase text-center mb-2" style={{ fontSize: '1.3rem' }}>Create an account</h2>
                        <form onSubmit={handleRegister}>
                            <div style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>
                                <MDBInput
                                    placeholder='Username'
                                    id='form1'
                                    type='text'
                                    value={username}
                                    onChange={(e) => setUsername(e.target.value)}
                                    required
                                    style={{ fontSize: '0.9rem' }}
                                />
                                <MDBInput
                                    placeholder='Full Name'
                                    id='form2'
                                    type='text'
                                    value={fullName}
                                    onChange={(e) => setFullName(e.target.value)}
                                    required
                                    style={{ fontSize: '0.9rem' }}
                                />
                                <MDBInput
                                    placeholder='Last Name'
                                    id='form3'
                                    type='text'
                                    value={lastName}
                                    onChange={(e) => setLastName(e.target.value)}
                                    required
                                    style={{ fontSize: '0.9rem' }}
                                />
                                <MDBInput
                                    placeholder='Your Email'
                                    id='form4'
                                    type='email'
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                    required
                                    style={{ fontSize: '0.9rem' }}
                                />
                                <MDBInput
                                    placeholder='Password'
                                    id='form5'
                                    type='password'
                                    value={password}
                                    onChange={(e) => setPassword(e.target.value)}
                                    required
                                    style={{ fontSize: '0.9rem' }}
                                />
                                <MDBInput
                                    placeholder='Repeat your password'
                                    id='form6'
                                    type='password'
                                    value={confirmPassword}
                                    onChange={(e) => setConfirmPassword(e.target.value)}
                                    required
                                    style={{ fontSize: '0.9rem' }}
                                />
                                <MDBInput
                                    placeholder='Date of Birth'
                                    id='form7'
                                    type='date'
                                    value={dateOfBirth}
                                    onChange={(e) => setDateOfBirth(e.target.value)}
                                    required
                                    style={{ fontSize: '0.9rem' }}
                                />

                                {passwordMatchError && <div className="text-danger mb-2" style={{ fontSize: '0.8rem' }}>{passwordMatchError}</div>}
                                {emailTakenError && <div className="text-danger mb-2" style={{ fontSize: '0.8rem' }}>{emailTakenError}</div>}
                                {errorMessage && <div className="text-danger mb-2" style={{ fontSize: '0.8rem' }}>{errorMessage}</div>}

                                <MDBBtn
                                    className='register-btn'
                                    size='lg'
                                    type='submit'
                                    style={{
                                        width: '120px',
                                        height: '40px',
                                        alignSelf: 'center',
                                        fontSize: '0.9rem',
                                    }}
                                >Register</MDBBtn>
                            </div>
                        </form>
                        <NavLink to="/LoginForm" className="text-center d-block mt-2" style={{ color: 'green', fontSize: '0.9rem' }}>Already have an account? Log in here</NavLink>
                    </MDBCardBody>
                </MDBCard>
            </div>
        </div>
    );
}

export default Register;
