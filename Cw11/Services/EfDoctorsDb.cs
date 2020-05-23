using Cw11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw11.Services
{
    public class EfDoctorsDb : IDoctorsDb
    {
        private readonly CodeFirstContext _context;
        public EfDoctorsDb(CodeFirstContext context)
        {
            _context = context;
        }

        public Doctor AddDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            return doctor;
        }

        public void DeleteDoctor(int id)
        {
            var doc = _context.Doctors.FirstOrDefault(doc => doc.IdDoctor == id);
            if (doc == null)
            {
                throw new Exception();
            }
            _context.Doctors.Remove(doc);
            _context.SaveChanges();
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            return _context.Doctors.ToList();
        }

        public Doctor ModifyDoctor(Doctor modDoc)
        {
            var doc = _context.Doctors.FirstOrDefault(d => d.IdDoctor == modDoc.IdDoctor);
            if (doc == null)
            {
                throw new Exception();
            }

            doc.FirstName = modDoc.FirstName;
            doc.LastName = modDoc.LastName;
            doc.Email = modDoc.Email;

            _context.Doctors.Update(doc);
            _context.SaveChanges();

            return doc;
        }

        public void Seed()
        {
            var doc1 = new Doctor
            {
                FirstName = "Imie1",
                LastName = "Nazwisko1",
                Email = "e1@pjwstk.edu.pl"
            };
            var doc2 = new Doctor
            {
                FirstName = "Imie2",
                LastName = "Nazwisko2",
                Email = "e2@pjwstk.edu.pl"
            };

            var pat1 = new Patient
            {
                FirstName = "Imie3",
                LastName = "Nazwisko3",
                BirthDate = DateTime.Today
            };

            var pat2 = new Patient
            {
                FirstName = "Imie4",
                LastName = "Nazwisko4",
                BirthDate = DateTime.Today
            };

            var pre1 = new Prescription
            {
                Date = DateTime.Today,
                DueDate = DateTime.Today,
                IdPatient = 1,
                IdDoctor = 2
            };

            var pre2 = new Prescription
            {
                Date = DateTime.Today,
                DueDate = DateTime.Today,
                IdPatient = 2,
                IdDoctor = 1
            };

            var med1 = new Medicament
            {
                Name = "Lek1",
                Description = "desc1",
                Type = "typ1"
            };

            var med2 = new Medicament
            {
                Name = "Lek2",
                Description = "desc2",
                Type = "typ2"
            };

            var pm1 = new PrescriptionMedicament
            {
                IdMedicament = 1,
                IdPrescription = 2,
                Dose = 4,
                Details = "deets"
            };

            var pm2 = new PrescriptionMedicament
            {
                IdMedicament = 2,
                IdPrescription = 1,
                Details = "details"
            };

            _context.Doctors.Add(doc1);
            _context.Doctors.Add(doc2);

            _context.Patient.Add(pat1);
            _context.Patient.Add(pat2);


            _context.Prescription.Add(pre1);
            _context.Prescription.Add(pre2);

            _context.Medicaments.Add(med1);
            _context.Medicaments.Add(med2);

            _context.PrescriptionMedicament.Add(pm1);
            _context.PrescriptionMedicament.Add(pm2);
        }
    }
}

