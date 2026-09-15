import { FaWhatsapp } from 'react-icons/fa';
import '../../styles/floating-whatsapp.css';

export default function FloatingWhatsApp() {
  return (
    <a
      href="https://wa.me/919572003492?text=Hi%20Subhash%2C%20I%20saw%20your%20portfolio!"
      target="_blank"
      rel="noopener noreferrer"
      className="float-wa"
      aria-label="Chat on WhatsApp"
      title="Chat with me on WhatsApp"
    >
      <span className="float-wa-icon">
        <FaWhatsapp size={26} />
      </span>
      <span className="float-wa-text">Chat With Me</span>
    </a>
  );
}