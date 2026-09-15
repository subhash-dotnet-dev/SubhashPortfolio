import { useEffect, useState } from 'react';
import { useEducation } from '../../../hooks/useEducation';
import {
  Sparkles,
  GraduationCap,
  Calendar,
  Award,
  BookOpen,
  MapPin,
  TrendingUp,
  Star,
} from 'lucide-react';
import '../../../styles/education.css';

const LEVEL_INFO: Record<number, { label: string; color: string; rank: string }> = {
  1: { label: 'Secondary', color: '#10b981', rank: 'Foundation' },
  2: { label: 'Senior Secondary', color: '#22d3ee', rank: 'Intermediate' },
  3: { label: 'Bachelors', color: '#3b82f6', rank: 'Undergraduate' },
  4: { label: 'Masters', color: '#8b5cf6', rank: 'Postgraduate' },
  5: { label: 'Doctorate', color: '#f59e0b', rank: 'Research' },
};

function GradeRing({ value, color }: { value: string; color: string }) {
  const [animated, setAnimated] = useState(false);

  useEffect(() => {
    const t = setTimeout(() => setAnimated(true), 300);
    return () => clearTimeout(t);
  }, []);

  // Parse "73.34%" -> 73.34
  const numeric = parseFloat(value.replace('%', '')) || 0;
  const pct = Math.min(numeric, 100);

  const size = 78;
  const stroke = 5;
  const radius = (size - stroke) / 2;
  const circ = 2 * Math.PI * radius;
  const offset = circ - (animated ? pct / 100 : 0) * circ;

  return (
    <div className="ed-ring-wrap" style={{ '--c': color } as React.CSSProperties}>
      <svg width={size} height={size} className="ed-ring">
        <circle
          cx={size / 2}
          cy={size / 2}
          r={radius}
          fill="none"
          stroke="currentColor"
          strokeWidth={stroke}
          className="ed-ring-track"
          opacity="0.12"
        />
        <circle
          cx={size / 2}
          cy={size / 2}
          r={radius}
          fill="none"
          stroke={color}
          strokeWidth={stroke}
          strokeDasharray={circ}
          strokeDashoffset={offset}
          strokeLinecap="round"
          transform={`rotate(-90 ${size / 2} ${size / 2})`}
          className="ed-ring-fill"
        />
      </svg>
      <div className="ed-ring-content">
        <span className="ed-ring-value">{value}</span>
        <span className="ed-ring-label">Grade</span>
      </div>
    </div>
  );
}

export default function Education() {
  const { education } = useEducation();

  const sorted = [...education].sort(
    (a, b) => a.displayOrder - b.displayOrder
  );

  return (
    <section id="education" className="ed-sec">

      {/* ============ BACKGROUND ============ */}
      <div className="ed-bg" aria-hidden="true">
        <div className="ed-bg-base" />
        <div className="ed-bg-grid" />
        <div className="ed-bg-orb ed-bg-orb-1" />
        <div className="ed-bg-orb ed-bg-orb-2" />
        <div className="ed-bg-orb ed-bg-orb-3" />
        <div className="ed-bg-particles">
          {Array.from({ length: 30 }).map((_, i) => (
            <span
              key={i}
              className="ed-particle"
              style={{
                left: `${(i * 37) % 100}%`,
                top: `${(i * 53) % 100}%`,
                animationDelay: `${(i * 0.3) % 5}s`,
                animationDuration: `${6 + (i % 5)}s`,
              }}
            />
          ))}
        </div>
        <div className="ed-bg-aurora" />
        <div className="ed-bg-conic" />
        <div className="ed-bg-vignette" />
      </div>

      <div className="ed-wrap">

        {/* ============ HEADER ============ */}
        <header className="ed-hd">
          <span className="ed-pill">
            <Sparkles size={10} />
            <span>Education</span>
          </span>
          <h2 className="ed-title">
            My{' '}
            <span className="ed-title-accent">academic foundation</span>.
          </h2>
          <p className="ed-meta-line">
            <strong>{sorted.length}</strong> qualifications
            <span className="ed-meta-dot">/</span>
            <span>Building strong fundamentals</span>
          </p>
        </header>

        {/* ============ CARDS ============ */}
        <div className="ed-grid">
          {sorted.map((edu, idx) => {
            const info = LEVEL_INFO[edu.level] || LEVEL_INFO[3];
            const color = info.color;

            return (
              <article
                key={edu.id}
                className="ed-card"
                style={{ '--c': color } as React.CSSProperties}
              >
                {/* Glow background */}
                <div className="ed-card-glow" />

                {/* Shine sweep */}
                <div className="ed-card-shine" />

                {/* Floating particles */}
                {[0, 1, 2].map((p) => (
                  <span
                    key={p}
                    className={`ed-card-particle ed-card-particle--${p}`}
                  />
                ))}

                {/* Number badge */}
                <span className="ed-card-num">
                  {String(idx + 1).padStart(2, '0')}
                </span>

                {/* Level chip (top) */}
                <span className="ed-card-level-chip">
                  <Star size={9} />
                  {info.rank}
                </span>

                {/* Head row: Icon + Titles */}
                <header className="ed-card-hd">
                  <div className="ed-card-icon-wrap">
                    <div className="ed-card-icon-glow" />
                    <span className="ed-card-icon">
                      <GraduationCap size={22} />
                    </span>
                  </div>

                  <div className="ed-card-titles">
                    <h3 className="ed-card-degree">{edu.degree}</h3>
                    <span className="ed-card-institution">
                      <BookOpen size={10} />
                      {edu.institution}
                    </span>
                  </div>
                </header>

                {/* Meta pills */}
                <div className="ed-card-meta">
                  <span className="ed-meta-pill">
                    <Calendar size={10} />
                    {edu.startYear ? `${edu.startYear} – ` : ''}
                    {edu.completionYear}
                  </span>

                  {edu.board && (
                    <span className="ed-meta-pill">
                      <MapPin size={10} />
                      {edu.board}
                    </span>
                  )}

                  <span className="ed-meta-pill ed-meta-pill--level">
                    <Award size={10} />
                    {info.label}
                  </span>
                </div>

                {/* Grade ring section */}
                <div className="ed-card-grade-section">
                  <div className="ed-card-grade-info">
                    <span className="ed-grade-tag">
                      <TrendingUp size={11} />
                      Final Score
                    </span>
                    <p className="ed-grade-text">
                      Successfully completed with strong academic performance.
                    </p>
                  </div>

                  <GradeRing value={edu.gradeOrPercentage} color={color} />
                </div>

                {/* Level progress bar */}
                <div className="ed-card-level-bar">
                  <span className="ed-level-bar-track">
                    <span
                      className="ed-level-bar-fill"
                      style={{ width: `${(edu.level / 5) * 100}%` }}
                    />
                  </span>
                  <span className="ed-level-bar-text">
                    Level {edu.level} / 5
                  </span>
                </div>

              </article>
            );
          })}
        </div>

      </div>
    </section>
  );
}
