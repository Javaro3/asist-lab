import React from 'react';

const InputField = ({ type, placeholder, value, onChange }) => {
  return (
    <input
      type={type}
      placeholder={placeholder}
      value={value}
      onChange={onChange}
      className="mb-4 p-2 border rounded w-full"
    />
  );
};

export default InputField;
