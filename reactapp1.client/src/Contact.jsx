import { useState } from 'react';
import { useForm } from 'react-hook-form';
import emailjs from '@emailjs/browser';
import './contact.css';
import Footer from './Footer';

const Contact = () => {
    const {
        register,
        handleSubmit,
        reset,
        formState: { errors }
    } = useForm();
    const [disabled, setDisabled] = useState(false);
    const [alertInfo, setAlertInfo] = useState({
        display: false,
        message: '',
        type: ''
    });

    // Shows alert message for form submission feedback
    const toggleAlert = (message, type) => {
        setAlertInfo({ display: true, message, type });

        // Hide alert after 5 seconds
        setTimeout(() => {
            setAlertInfo({ display: false, message: '', type: '' });
        }, 5000);
    };

    // Function called on submit that uses emailjs to send email of valid contact form
    const onSubmit = async (data) => {
        // Destructure data object
        const { name, email, subject, message } = data;
        try {
            // Disable form while processing submission
            setDisabled(true);

            const templateParams = {
                name,
                email,
                subject,
                message
            };

            await emailjs.send(
                process.env.REACT_APP_SERVICE_ID,
                process.env.REACT_APP_TEMPLATE_ID,
                templateParams,
                process.env.REACT_APP_USER_ID
            );

            // Display success alert
            toggleAlert('Form submission was successful!', 'success');
        } catch (e) {
            console.error(e);
            // Display error alert
            toggleAlert('Uh oh. Something went wrong.', 'danger');
        } finally {
            // Re-enable form submission
            setDisabled(false);
            // Reset contact form fields after submission
            reset();
        }
    };

    return (
        <div className="contact-container">
            <div className='ContactForm'>
              
                <div className='container'>
                    <div className='row1'>   <h1 >Contact US</h1></div>
                    <div className='row'>
                      
                        <div className='col-12 text-center'>
                            <div className='contactForm'>
                                <form id='contact-form' onSubmit={handleSubmit(onSubmit)} noValidate>
                                    {/* Row 1 of form */}
                                    <div className='row formRow'>
                                        <div className='col'>
                                            <input
                                                type='text'
                                                name='name'
                                                {...register('name', {
                                                    required: { value: true, message: 'Please enter your name' },
                                                    maxLength: {
                                                        value: 30,
                                                        message: 'Please use 30 characters or less'
                                                    }
                                                })}
                                                className='form-control formInput'
                                                placeholder='Name'
                                            />
                                            {errors.name && <span className='errorMessage'>{errors.name.message}</span>}
                                        </div>
                                        <div className='col'>
                                            <input
                                                type='email'
                                                name='email'
                                                {...register('email', {
                                                    required: true,
                                                    pattern: /^[a-zA-Z0-9.!#$%&�*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/
                                                })}
                                                className='form-control formInput'
                                                placeholder='Email address'
                                            />
                                            {errors.email && (
                                                <span className='errorMessage'>Please enter a valid email address</span>
                                            )}
                                        </div>
                                    </div>
                                    {/* Row 2 of form */}
                                    <div className='row formRow'>
                                        <div className='col'>
                                            <input
                                                type='text'
                                                name='subject'
                                                {...register('subject', {
                                                    required: { value: true, message: 'Please enter a subject' },
                                                    maxLength: {
                                                        value: 75,
                                                        message: 'Subject cannot exceed 75 characters'
                                                    }
                                                })}
                                                className='form-control formInput'
                                                placeholder='Subject'
                                            />
                                            {errors.subject && (
                                                <span className='errorMessage'>{errors.subject.message}</span>
                                            )}
                                        </div>
                                    </div>
                                    {/* Row 3 of form */}
                                    <div className='row formRow'>
                                        <div className='col'>
                                            <textarea
                                                rows={3}
                                                name='message'
                                                {...register('message', {
                                                    required: true
                                                })}
                                                className='form-control formInput'
                                                placeholder='Message'
                                            />
                                            {errors.message && <span className='errorMessage'>Please enter a message</span>}
                                        </div>
                                    </div>
                                    <button className='submit-btn' type='submit'>
                                        Submit
                                    </button>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
                {alertInfo.display && (
                    <div className={`alert alert-${alertInfo.type} alert-dismissible mt-5`} role='alert'>
                        {alertInfo.message}
                        <button
                            type='button'
                            className='btn-close'
                            data-bs-dismiss='alert'
                            aria-label='Close'
                            onClick={() => setAlertInfo({ display: false, message: '', type: '' })}
                        />
                    </div>
                )}
            </div>
            <Footer />
        </div>
    );
};

export default Contact;
