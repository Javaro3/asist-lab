import React from 'react';
import Modal from 'react-modal';

const ImageModal = ({ isModalOpen, closeModal, images }) => {
  return (
    <Modal
      isOpen={isModalOpen}
      onRequestClose={closeModal}
      style={{
        overlay: {
          zIndex: 1000,
        },
        content: {
          top: '50%',
          left: '50%',
          right: 'auto',
          bottom: 'auto',
          marginRight: '-50%',
          marginBottom: '-50%',
          transform: 'translate(-50%, -50%)',
          width: '600px',
          height: '800px', 
          overflowY: 'auto',
          zIndex: 1000,
        },
      }}
    >
      <h2 className="text-lg mb-4">Images</h2>
      <div className="flex flex-col gap-4">
        {images.map((image) => (
          <img
            key={image.id}
            src={`https://localhost:7213/Image/get?id=${image.id}`}
            alt={`Trip Image ${image.id}`}
            className="cursor-pointer w-full h-auto"
            onClick={() => window.open(image.url, '_blank')}
          />
        ))}
      </div>
    </Modal>
  );
};

export default ImageModal;
