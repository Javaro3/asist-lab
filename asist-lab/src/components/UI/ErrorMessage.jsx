import React from 'react';

const ErrorMessage = ({ message }) => {
  return message ? <p className="text-red-500 mb-4">{message}</p> : null;
};

export default ErrorMessage;
