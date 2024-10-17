import React, { useState, useEffect } from 'react';
import { MDBBtn, MDBCard, MDBCardBody, MDBInput } from 'mdb-react-ui-kit';
import { NavLink } from 'react-router-dom';
import contactImage from './assets/R.jpeg'; // Ensure the image path is correct
import Header from './Header'; // Import the Header component

function Register() {
    document.title = "Register";

    // Redirect if user is already registered
    useEffect(() => {
        const user = localStorage.getItem("user");
        if (user) {
            document.location = "/";
        }
    }, []);

    const [role, setRole] = useState('');
    const [fullName, setFullName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [dateOfBirth, setDateOfBirth] = useState(''); // New state for date of birth
    const [errorMessage, setErrorMessage] = useState('');
    const [passwordMatchError, setPasswordMatchError] = useState('');
    const [emailTakenError, setEmailTakenError] = useState('');

    const handleRoleChange = (event) => setRole(event.target.value);
    const handleFullNameChange = (event) => setFullName(event.target.value);
    const handleEmailChange = (event) => setEmail(event.target.value);
    const handlePasswordChange = (event) => setPassword(event.target.value);
    const handleConfirmPasswordChange = (event) => setConfirmPassword(event.target.value);
    const handleDateOfBirthChange = (event) => setDateOfBirth(event.target.value); // Handle date of birth change

    const handleRegister = async (event) => {
        event.preventDefault();

        // Reset errors
        setPasswordMatchError('');
        setEmailTakenError('');
        setErrorMessage('');

        if (password !== confirmPassword) {
            setPasswordMatchError('Passwords do not match');
            return;
        }

        if (role !== 'Admin' && role !== 'Doctor' && role !== 'User') {
            setErrorMessage("Role doesn't exist");
            return;
        }

        const userData = {
            role: role,
            email: email,
            password: password,
            confirmPassword: confirmPassword,
            fullName: fullName, // Include full name
            dateOfBirth: dateOfBirth // Include date of birth
        };

        try {
            const response = await fetch(`https://localhost:7107/api/Auth/Register?role=${role}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(userData),
            });

            if (!response.ok) {
                const errorData = await response.json();
                console.error('Registration failed:', errorData);

                if (errorData.title === 'Email already taken') {
                    setEmailTakenError('Email is already taken!');
                } else if (errorData.title === 'Passwords do not match') {
                    setPasswordMatchError('Passwords do not match');
                } else {
                    setErrorMessage(errorData.title || 'Registration failed. Please try again.');
                }
                return;
            }

            const data = await response.json();
            console.log('Registration successful:', data);
            document.location = "/login"; // Redirect to login on successful registration

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
                padding: '10px' // Reduced padding
            }}>
                <MDBCard className='p-3' style={{
                    maxWidth: '400px', // Reduced max width
                    width: '100%',
                    backgroundColor: 'rgba(255, 255, 255, 0.9)',
                    borderRadius: '10px',
                    boxShadow: '0px 0px 5px rgba(0, 0, 0, 0.1)'
                }}>
                    <MDBCardBody className='px-4 py-3'>
                        <h2 className="text-uppercase text-center mb-2" style={{ fontSize: '1.3rem' }}>Create an account</h2>
                        <form onSubmit={handleRegister}>
                            <div style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>
                                <MDBInput
                                    placeholder='Role'
                                    id='form6'
                                    type='text'
                                    value={role}
                                    onChange={handleRoleChange}
                                    required
                                    style={{ fontSize: '0.9rem' }}
                                />
                                <MDBInput
                                    placeholder='Your Name'
                                    id='form1'
                                    type='text'
                                    value={fullName}
                                    onChange={handleFullNameChange}
                                    required
                                    style={{ fontSize: '0.9rem' }}
                                />
                                <MDBInput
                                    placeholder='Your Email'
                                    id='form2'
                                    type='email'
                                    value={email}
                                    onChange={handleEmailChange}
                                    required
                                    style={{ fontSize: '0.9rem' }}
                                />
                                <MDBInput
                                    placeholder='Password'
                                    id='form3'
                                    type='password'
                                    value={password}
                                    onChange={handlePasswordChange}
                                    required
                                    style={{ fontSize: '0.9rem' }}
                                />
                                <MDBInput
                                    placeholder='Repeat your password'
                                    id='form4'
                                    type='password'
                                    value={confirmPassword}
                                    onChange={handleConfirmPasswordChange}
                                    required
                                    style={{ fontSize: '0.9rem' }}
                                />
                                <MDBInput
                                    placeholder='Date of Birth'
                                    id='form5'
                                    type='date'
                                    value={dateOfBirth}
                                    onChange={handleDateOfBirthChange}
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
                                        width: '120px', // Reduced width
                                        height: '40px', // Reduced height
                                        alignSelf: 'center',
                                        fontSize: '0.9rem', // Smaller font size
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
