namespace :db do
  desc "Llena la base de datos con datos de ejemplo"
  task populate: :environment do

    User.create!(nombre_usuario: "admin",
                 email: "prueba@prueba.com",
                 password: "desarrollo",
                 password_confirmation: "desarrollo")
    9.times do |i|
      User.create!(nombre_usuario: Faker::Internet.user_name,
                   email: Faker::Internet.email,
                   password: "desarrollo",
                   password_confirmation: "desarrollo")
    end

    Estado.create!(nombre: 'Pendiente')
    Estado.create!(nombre: 'Falta información')
    Estado.create!(nombre: 'En proceso')
    Estado.create!(nombre: 'Terminado')
    Estado.create!(nombre: 'Cerrado')
    Estado.create!(nombre: 'Rechazado')
    Estado.create!(nombre: 'Corrección')
    10.times do |i|
      cliente = Cliente.create!(nombre: Faker::Company.name)
      rand(1..4).times do |j|
        proyecto = cliente.proyectos.create!(nombre: Faker::Lorem.sentence(3))
        rand(7).times do |k|
          tarea = proyecto.tareas.new(descripcion: Faker::Lorem.sentence(rand(10..20)))
          tarea.estado = Estado.first(:order => "RANDOM()")
          tarea.user = User.first(:order => "RANDOM()")
          tarea.save
        end
      end
    end
  end
end