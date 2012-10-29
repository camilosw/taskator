namespace :db do
  desc "Populate the database with sample data"
  task populate: :environment do

    9.times do |i|
      User.create!(username: Faker::Internet.user_name,
                   email: Faker::Internet.email,
                   password: "password",
                   password_confirmation: "password")
    end

    10.times do |i|
      client = Client.create!(name: Faker::Company.name, 
                              closed: [true, false].sample)
      rand(1..4).times do |j|
        project = client.projects.create!(name: Faker::Lorem.sentence(1))
        rand(7).times do |k|
          task = project.tasks.new(description: Faker::Lorem.sentence(rand(10..20)), 
                                  closed: [true, false].sample)
          task.creator = User.first(:order => "RANDOM()")
          task.assigned = User.first(:order => "RANDOM()") if [true, false].sample
          task.created_at = (rand*10).days.ago - 5.days
          task.updated_at = task.created_at + (rand*5).days
          task.due_date = task.created_at + (rand*10).days if [true, false].sample
          task.save
        end
      end
    end
  end
end