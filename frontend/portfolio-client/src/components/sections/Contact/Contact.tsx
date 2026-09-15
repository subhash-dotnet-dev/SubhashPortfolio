import { useState } from 'react';
import { useProfile } from '../../../hooks/useProfile';
import { useSocialLinks } from '../../../hooks/useSocialLinks';
import { contactService } from '../../../services/contactService';
import type { ContactFormData } from '../../../types';
import {
  Sparkles,
  Send,
  Mail,
  Phone,
  MapPin,
  User,
  MessageSquare,
  Tag,
  CheckCircle2,
  AlertCircle,
  Loader2,
  Clock,
  ArrowUpRight,
  Shield,
  Globe,
  Zap,
} from 'lucide-react';
import { FaGithub, FaLinkedin, FaInstagram, FaFacebook } from 'react-icons/fa';
import { SiLeetcode, SiHackerrank } from 'react-icons/si';
import '../../../styles/contact.css';

const iconMap = {
  1: FaGithub,
  2: FaLinkedin,
  3: SiLeetcode,
  4: SiHackerrank,
  5: FaInstagram,
  6: FaFacebook,
};

const INITIAL: ContactFormData = {
  name: '',
  email: '',
  phone: '',
  subject: '',
  message: '',
};

export default function Contact() {
  const { profile } = useProfile();
  const { links } = useSocialLinks();

  const [form, setForm] = useState<ContactFormData>(INITIAL);
  const [errors, setErrors] = useState<Partial<ContactFormData>>({});
  const [status, setStatus] = useState<'idle' | 'loading' | 'success' | 'error'>('idle');
  const [feedback, setFeedback] = useState('');

  const email = profile?.email || 'subhash.dev79@gmail.com';
  const phone = profile?.phone || '+91 9572003492';
  const location = profile?.location || 'Ameerpet, Hyderabad, India';

  const update = (field: keyof ContactFormData, value: string) => {
    setForm((f) => ({ ...f, [field]: value }));
    if (errors[field]) setErrors((e) => ({ ...e, [field]: undefined }));
    if (status === 'success' || status === 'error') {
      setStatus('idle');
      setFeedback('');
    }
  };

  const validate = (): boolean => {
    const e: Partial<ContactFormData> = {};
    if (!form.name.trim()) e.name = 'Name is required';
    else if (form.name.trim().length < 2) e.name = 'Min 2 characters';

    if (!form.email.trim()) e.email = 'Email is required';
    else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.email)) e.email = 'Invalid email';

    if (!form.phone.trim()) e.phone = 'Phone required';
    else if (!/^[\d\s+\-()]{7,}$/.test(form.phone)) e.phone = 'Invalid phone';

    if (!form.subject.trim()) e.subject = 'Subject required';
    if (!form.message.trim()) e.message = 'Message required';
    else if (form.message.trim().length < 10) e.message = 'Min 10 characters';

    setErrors(e);
    return Object.keys(e).length === 0;
  };

  const handleSubmit = async (ev: React.FormEvent) => {
    ev.preventDefault();
    if (status === 'loading') return;
    if (!validate()) return;

    setStatus('loading');
    setFeedback('');

    const res = await contactService.submit(form);

    if (res.success) {
      setStatus('success');
      setFeedback(res.message);
      setForm(INITIAL);
      setTimeout(() => {
        setStatus('idle');
        setFeedback('');
      }, 6000);
    } else {
      setStatus('error');
      setFeedback(res.message);
    }
  };

  return (
    <section id="contact" className="ct-sec">

      {/* ============ BACKGROUND ============ */}
      <div className="ct-bg" aria-hidden="true">
        <div className="ct-bg-base" />
        <div className="ct-bg-grid" />
        <div className="ct-bg-orb ct-bg-orb-1" />
        <div className="ct-bg-orb ct-bg-orb-2" />
        <div className="ct-bg-orb ct-bg-orb-3" />
        <div className="ct-bg-conic" />
        <div className="ct-bg-particles">
          {Array.from({ length: 30 }).map((_, i) => (
            <span
              key={i}
              className="ct-particle"
              style={{
                left: `${(i * 37) % 100}%`,
                top: `${(i * 53) % 100}%`,
                animationDelay: `${(i * 0.3) % 5}s`,
                animationDuration: `${6 + (i % 5)}s`,
              }}
            />
          ))}
        </div>
        <div className="ct-bg-aurora" />
        <div className="ct-bg-vignette" />
      </div>

      <div className="ct-wrap">

        {/* ============ HEADER ============ */}
        <header className="ct-hd">
          <span className="ct-pill">
            <Sparkles size={10} />
            <span>Get in Touch</span>
          </span>
          <h2 className="ct-title">
            Let&apos;s build something{' '}
            <span className="ct-title-accent">remarkable</span>.
          </h2>
          <p className="ct-desc">
            Have a project in mind or just want to say hi? I&apos;m always open
            to discussing new ideas.
          </p>
        </header>

        {/* ============ MAIN GRID ============ */}
        <div className="ct-grid">

          {/* ============ LEFT: INFO ============ */}
          <aside className="ct-info">

            {/* Quick actions */}
            <div className="ct-quick">
              <a
                href={`mailto:${email}`}
                className="ct-quick-card ct-quick-card--email"
              >
                <span className="ct-quick-icon">
                  <Mail size={16} />
                </span>
                <div className="ct-quick-body">
                  <span className="ct-quick-label">Email</span>
                  <span className="ct-quick-value">{email}</span>
                </div>
                <span className="ct-quick-arrow">
                  <ArrowUpRight size={13} />
                </span>
              </a>

              <a
                href={`tel:${phone.replace(/\s+/g, '')}`}
                className="ct-quick-card ct-quick-card--phone"
              >
                <span className="ct-quick-icon">
                  <Phone size={16} />
                </span>
                <div className="ct-quick-body">
                  <span className="ct-quick-label">Call</span>
                  <span className="ct-quick-value">{phone}</span>
                </div>
                <span className="ct-quick-arrow">
                  <ArrowUpRight size={13} />
                </span>
              </a>
            </div>

            {/* Details */}
            <div className="ct-details">
              <div className="ct-detail-row">
                <span className="ct-detail-icon">
                  <MapPin size={13} />
                </span>
                <div className="ct-detail-body">
                  <span className="ct-detail-lbl">Location</span>
                  <span className="ct-detail-val">{location}</span>
                </div>
              </div>

              <div className="ct-detail-row">
                <span className="ct-detail-icon ct-detail-icon--live">
                  <Clock size={13} />
                </span>
                <div className="ct-detail-body">
                  <span className="ct-detail-lbl">Status</span>
                  <span className="ct-detail-val ct-detail-val--live">
                    <span className="ct-live-dot" />
                    Available for work
                  </span>
                </div>
              </div>

              <div className="ct-detail-row">
                <span className="ct-detail-icon">
                  <Zap size={13} />
                </span>
                <div className="ct-detail-body">
                  <span className="ct-detail-lbl">Response</span>
                  <span className="ct-detail-val">Within 24 hours</span>
                </div>
              </div>
            </div>

            {/* Socials */}
            {links.length > 0 && (
              <div className="ct-socials">
                <span className="ct-socials-lbl">
                  <Globe size={12} />
                  Connect
                </span>
                <div className="ct-socials-list">
                  {links.map((l) => {
                    const Icon = iconMap[l.platform as keyof typeof iconMap];
                    if (!Icon) return null;
                    const slug = l.displayName.toLowerCase().replace(/\s+/g, '');
                    return (
                      <a
                        key={l.id}
                        href={l.url}
                        target="_blank"
                        rel="noopener noreferrer"
                        className={`ct-social ct-social--${slug}`}
                        aria-label={l.displayName}
                        title={l.displayName}
                      >
                        <span className="ct-social-icon">
                          <Icon size={20} />
                        </span>
                        <span className="ct-social-name">{l.displayName}</span>
                      </a>
                    );
                  })}
                </div>
              </div>
            )}
          </aside>

          {/* ============ RIGHT: FORM ============ */}
          <form className="ct-form" onSubmit={handleSubmit} noValidate>

            <header className="ct-form-hd">
              <h3 className="ct-form-title">
                <Send size={15} />
                Send a Message
              </h3>
              <span className="ct-form-badge">
                <span className="ct-form-badge-dot" />
                All fields required
              </span>
            </header>

            <div className="ct-form-row">
              <div className="ct-field">
                <label className="ct-label" htmlFor="ct-name">
                  <User size={10} />
                  Name
                </label>
                <input
                  id="ct-name"
                  type="text"
                  className={`ct-input ${errors.name ? 'has-error' : ''}`}
                  placeholder="Your name"
                  value={form.name}
                  onChange={(e) => update('name', e.target.value)}
                />
                {errors.name && (
                  <span className="ct-error">
                    <AlertCircle size={9} />
                    {errors.name}
                  </span>
                )}
              </div>

              <div className="ct-field">
                <label className="ct-label" htmlFor="ct-email">
                  <Mail size={10} />
                  Email
                </label>
                <input
                  id="ct-email"
                  type="email"
                  className={`ct-input ${errors.email ? 'has-error' : ''}`}
                  placeholder="you@example.com"
                  value={form.email}
                  onChange={(e) => update('email', e.target.value)}
                />
                {errors.email && (
                  <span className="ct-error">
                    <AlertCircle size={9} />
                    {errors.email}
                  </span>
                )}
              </div>
            </div>

            <div className="ct-form-row">
              <div className="ct-field">
                <label className="ct-label" htmlFor="ct-phone">
                  <Phone size={10} />
                  Phone
                </label>
                <input
                  id="ct-phone"
                  type="tel"
                  className={`ct-input ${errors.phone ? 'has-error' : ''}`}
                  placeholder="+91 98765 43210"
                  value={form.phone}
                  onChange={(e) => update('phone', e.target.value)}
                />
                {errors.phone && (
                  <span className="ct-error">
                    <AlertCircle size={9} />
                    {errors.phone}
                  </span>
                )}
              </div>

              <div className="ct-field">
                <label className="ct-label" htmlFor="ct-subject">
                  <Tag size={10} />
                  Subject
                </label>
                <input
                  id="ct-subject"
                  type="text"
                  className={`ct-input ${errors.subject ? 'has-error' : ''}`}
                  placeholder="What's it about?"
                  value={form.subject}
                  onChange={(e) => update('subject', e.target.value)}
                />
                {errors.subject && (
                  <span className="ct-error">
                    <AlertCircle size={9} />
                    {errors.subject}
                  </span>
                )}
              </div>
            </div>

            <div className="ct-field">
              <label className="ct-label" htmlFor="ct-message">
                <MessageSquare size={10} />
                Message
              </label>
              <textarea
                id="ct-message"
                rows={4}
                className={`ct-input ct-textarea ${errors.message ? 'has-error' : ''}`}
                placeholder="Tell me about your project..."
                value={form.message}
                onChange={(e) => update('message', e.target.value)}
              />
              {errors.message && (
                <span className="ct-error">
                  <AlertCircle size={9} />
                  {errors.message}
                </span>
              )}
            </div>

            {feedback && (
              <div className={`ct-feedback ct-feedback--${status}`}>
                {status === 'success' ? (
                  <CheckCircle2 size={13} />
                ) : (
                  <AlertCircle size={13} />
                )}
                <span>{feedback}</span>
              </div>
            )}

            <button
              type="submit"
              className="ct-submit"
              disabled={status === 'loading'}
            >
              {status === 'loading' ? (
                <>
                  <Loader2 size={14} className="ct-spin" />
                  <span>Sending...</span>
                </>
              ) : (
                <>
                  <Send size={14} />
                  <span>Send Message</span>
                  <ArrowUpRight size={13} className="ct-submit-arrow" />
                </>
              )}
            </button>

            <p className="ct-form-note">
              <Shield size={10} />
              Your information stays private and secure
            </p>

          </form>
        </div>

      </div>
    </section>
  );
}
