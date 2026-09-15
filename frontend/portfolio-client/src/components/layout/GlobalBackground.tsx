export default function GlobalBackground() {
  return (
    <div className="global-bg" aria-hidden="true">

      <div className="gb-base" />

      <div className="gb-mesh">
        <span className="gb-orb gb-orb-1" />
        <span className="gb-orb gb-orb-2" />
        <span className="gb-orb gb-orb-3" />
        <span className="gb-orb gb-orb-4" />
        <span className="gb-orb gb-orb-5" />
      </div>

      <div className="gb-conic" />
      <div className="gb-dots" />
      <div className="gb-lines" />
      <div className="gb-aurora" />

      <div className="gb-particles">
        {Array.from({ length: 12 }).map((_, i) => (
          <span
            key={i}
            className="gb-particle"
            style={{
              left: `${(i * 17) % 100}%`,
              top: `${(i * 29) % 100}%`,
              animationDelay: `${(i * 0.7) % 6}s`,
              animationDuration: `${10 + (i % 5)}s`,
            }}
          />
        ))}
      </div>

      <div className="gb-spotlight" />
      <div className="gb-scan" />
      <div className="gb-vignette" />
    </div>
  );
}
