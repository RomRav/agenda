using agenda.Db;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace agenda.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            
        }
        [Key]
        public int IdAppointment { get; set; }
        [Required(ErrorMessage = "Veuillez saisir l'heure du rendez-vous.")]
        [Display(Name = "Heure du rendez-vous")]
        public DateTime DateHour { get; set; }
        [Required(ErrorMessage = "Veuillez saisir le sujet.")]
        [Display(Name = "Sujet")]
        public string Subject { get; set; } = null!;
        [Display(Name ="Courtier")]
        public int IdBroker { get; set; } = 0;
        [Display(Name = "Client")]
        public int IdCustomer { get; set; } = 0;
        [ForeignKey("IdBroker")]
        public virtual Broker IdBrokerNavigation { get; set; } = null!;
        [ForeignKey("IdCustomer")]
        public virtual Customer IdCustomerNavigation { get; set; } = null!;
        //Récupére dans la base de données la liste des rendez-vous du jour.
        public static List<Appointment> GetTodayAppointmentsList()
        {
            List<Appointment> appointments = new List<Appointment>();
            SqlConnection conn = Connexion.open();
            string query = "SELECT * FROM appointments " +
               "INNER JOIN brokers ON brokers.idBroker = appointments.idBroker " +
               "INNER JOIN customers ON customers.idCustomer = appointments.idBroker " +
               "WHERE CONVERT(DATE, dateHour) = CONVERT(DATE, GETDATE()); ";
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            Appointment appointment = new Appointment();
                            appointment.IdAppointment = rdr.GetInt32(0);
                            appointment.DateHour = rdr.GetDateTime(1);
                            appointment.Subject = rdr.GetString(2);
                            appointment.IdBroker = rdr.GetInt32(3);
                            appointment.IdCustomer = rdr.GetInt32(4);
                            appointment.IdBrokerNavigation = new Broker
                            {
                                IdBroker = rdr.GetInt32(5),
                                Lastname = rdr.GetString(6),
                                Firstname = rdr.GetString(7),
                                Mail = rdr.GetString(8),
                                PhoneNumber = rdr.GetString(9)
                            };
                            appointment.IdCustomerNavigation = new Customer
                            {
                                IdCustomer = rdr.GetInt32(10),
                                Lastname = rdr.GetString(11),
                                Firstname = rdr.GetString(12),
                                Mail = rdr.GetString(13),
                                PhoneNumber = rdr.GetString(14),
                                Budget = rdr.GetInt32(15)
                            };
                            appointments.Add(appointment);
                        }
                    }
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                Connexion.close(conn);
            }
            return appointments;
        }
        //Récupére dans la base de données la liste des rendez-vous du jour.
        public static Appointment GetAppointmentsById(int id)
        {
            Appointment appointment = new Appointment();
            SqlConnection conn = Connexion.open();
            string query = "SELECT * FROM appointments " +
               "INNER JOIN brokers ON brokers.idBroker = appointments.idBroker " +
               "INNER JOIN customers ON customers.idCustomer = appointments.idBroker " +
               "WHERE idAppointment = @idAppointment; ";
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("idAppointment", id);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            appointment.IdAppointment = rdr.GetInt32(0);
                            appointment.DateHour = rdr.GetDateTime(1);
                            appointment.Subject = rdr.GetString(2);
                            appointment.IdBroker = rdr.GetInt32(3);
                            appointment.IdCustomer = rdr.GetInt32(4);
                            appointment.IdBrokerNavigation = new Broker
                            {
                                IdBroker = rdr.GetInt32(5),
                                Lastname = rdr.GetString(6),
                                Firstname = rdr.GetString(7),
                                Mail = rdr.GetString(8),
                                PhoneNumber = rdr.GetString(9)
                            };
                            appointment.IdCustomerNavigation = new Customer
                            {
                                IdCustomer = rdr.GetInt32(10),
                                Lastname = rdr.GetString(11),
                                Firstname = rdr.GetString(12),
                                Mail = rdr.GetString(13),
                                PhoneNumber = rdr.GetString(14),
                                Budget = rdr.GetInt32(15)
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                Connexion.close(conn);
            }
            return appointment;
        }
        //Verifie la disponibilité de l'heure du rendez-vous envoyé en paramétre, renvoie true si  l'heure est disponible.
        public static bool CheckAvalableAppointmentTime(Appointment appointment)
        {
            bool isOk = true;
            SqlConnection conn = Connexion.open();
            string query = "SELECT * FROM appointments WHERE dateHour = @date AND idBroker = @idBroker;";
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@date", appointment.DateHour);
                    cmd.Parameters.AddWithValue("@idBroker", appointment.IdBroker);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            if(appointment.DateHour == rdr.GetDateTime(1))
                            {
                                isOk = false;
                            }
                            else
                            {
                                isOk = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                Connexion.close(conn);
            }
            return isOk;
        }
    }
}
